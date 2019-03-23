using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public partial class GameSession : BaseNotificationClass
    {
        

        public event EventHandler<GameMessageEventArgs> OnMessageRaised;
        public Player CurrentPlayer { get; set; }

        public Weapon CurrentWeapon { get; set; }
        

        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "James",
                CharacterClass = "Figher",
                HitPoints = 10,
                ExperiencePoints = 0,
                Level = 1,
                Gold = 10000
            };
            
            if(!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }



        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
