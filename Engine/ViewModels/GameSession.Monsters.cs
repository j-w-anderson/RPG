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
                OnPropertyChanged();
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
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);

        
            // If monster is killed, collect rewards and loot
            if (CurrentMonster.IsDead)
            {
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
            }
        }
    }
}
