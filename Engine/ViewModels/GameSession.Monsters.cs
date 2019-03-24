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
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (CurrentMonster != null)
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);

            foreach (GameItem item in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {item.Name}.");
                CurrentPlayer.AddItemToInventory(item);
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
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} hit points!");
                CurrentMonster.TakeDamage(damageToMonster);
            }

            // If monster is killed, collect rewards and loot
            if (CurrentMonster.IsDead)
            {
                //RaiseMessage("");
                //RaiseMessage($"You defeated the {CurrentMonster.Name}!");

                //CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                //RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");

                //CurrentPlayer.Gold += CurrentMonster.Gold;
                //RaiseMessage($"You receive {CurrentMonster.Gold} gold.");

                //foreach (GameItem item in CurrentMonster.Inventory)
                //{
                //    CurrentPlayer.AddItemToInventory(item);
                //    RaiseMessage($"You receive one {item.Name}.");
                //}
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
                    RaiseMessage($"The {CurrentMonster.Name} hits you for {damageToPlayer} hit points.");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
                // If player die, move them to home.
                //if (CurrentPlayer.CurrentHitPoints <= 0)
                //{
                //    if (CurrentPlayer.HasAllTheseItems(new List<ItemQuantity> { new ItemQuantity(2001, 1) }))
                //    {
                //        CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(i => i.ItemTypeID == 2001));
                //        CurrentPlayer.CurrentHitPoints += 10;
                //        RaiseMessage("You quickly eat a healing herb to gain 10 hit points.");
                //    }
                //    else
                //    {
                //        RaiseMessage("");
                //        RaiseMessage($"The {CurrentMonster.Name} killed you.");
                //        CurrentLocation = CurrentWorld.LocationAt(0, -1);
                //        CurrentPlayer.CurrentHitPoints = CurrentPlayer.Level * 10;
                //    }
                //}
            }
        }
    }
}
