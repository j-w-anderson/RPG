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
        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLevelUp -= OnCurrentPlayerLevelUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLevelUp += OnCurrentPlayerLevelUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }

        private void OnCurrentPlayerLevelUp(object sender, System.EventArgs e)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}");
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs e)
        {
            //If player dies, move them to home.
            if (CurrentPlayer.HasAllTheseItems(new List<ItemQuantity> { new ItemQuantity(2001, 1) }))
            {
                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(i => i.ItemTypeID == 2001));
                CurrentPlayer.Heal(10);
                RaiseMessage("You quickly eat a healing herb to gain 10 hit points.");
            }
            else
            {
                RaiseMessage("");
                RaiseMessage($"The {CurrentMonster.Name} killed you.");
                CurrentLocation = CurrentWorld.LocationAt(0, -1);
                CurrentPlayer.CompletelyHeal();
            }
        }
    }
}
