using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public partial class GameSession
    {

        public bool HasMonster => CurrentMonster != null;

        private Monster _currentMonster;
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                _currentMonster = value;

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }


        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            // Determine damage to monster
            int damageToMonster = RandomNumberGenerator
                .NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage + 1);
            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else
            {
                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} hit points!");
            }

            // If monster is killed, collect rewards and loot
            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");

                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");

                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You receive {CurrentMonster.RewardGold} gold.");

                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You receive {itemQuantity.Quantity} {item.Name}.");
                }
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                //if monster is still alive, fight back
                int damageToPlayer = RandomNumberGenerator
                    .NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage + 1);
                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you!");
                }
                else
                {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hits you for {damageToPlayer} hit points.");
                }
                // If player dis, move them to home.
                if (CurrentPlayer.HitPoints <= 0)
                {
                    if (CurrentPlayer.HasAllTheseItems(new List<ItemQuantity> { new ItemQuantity(2001, 1) }))
                    {
                        CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(i=>i.ItemTypeID==2001));
                        CurrentPlayer.HitPoints += 10;
                        RaiseMessage("You quickly eat a healing herb to gain 10 hit points.");
                    }
                    else
                    {
                        RaiseMessage("");
                        RaiseMessage($"The {CurrentMonster.Name} killed you.");
                        CurrentLocation = CurrentWorld.LocationAt(0, -1);
                        CurrentPlayer.HitPoints = CurrentPlayer.Level * 10;
                    }
                }
            }
        }


    }
}
