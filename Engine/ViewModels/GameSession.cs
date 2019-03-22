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
    public class GameSession : BaseNotificationClass
    {
        public Player CurrentPlayer { get; set; }
        private Location _currentLocation;

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));

                GivePlayerQuestsAtLocation();
            }
        }


        public World CurrentWorld { get; set; }


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
            
            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                int xCoordinate = CurrentLocation.XCoordinate;
                int yCoordinate = CurrentLocation.YCoordinate;
                CurrentLocation = CurrentWorld.LocationAt(
                    xCoordinate,
                    yCoordinate + 1);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                int xCoordinate = CurrentLocation.XCoordinate;
                int yCoordinate = CurrentLocation.YCoordinate;
                CurrentLocation = CurrentWorld.LocationAt(
                    xCoordinate + 1,
                    yCoordinate);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                int xCoordinate = CurrentLocation.XCoordinate;
                int yCoordinate = CurrentLocation.YCoordinate;
                CurrentLocation = CurrentWorld.LocationAt(
                    xCoordinate - 1,
                    yCoordinate);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                int xCoordinate = CurrentLocation.XCoordinate;
                int yCoordinate = CurrentLocation.YCoordinate;
                CurrentLocation = CurrentWorld.LocationAt(
                    xCoordinate,
                    yCoordinate - 1);
            }
        }

        public bool HasLocationToNorth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate + 1) != null;
            }
        }
        public bool HasLocationToEast
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1,
                    CurrentLocation.YCoordinate) != null;
            }
        }
        public bool HasLocationToWest
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1,
                    CurrentLocation.YCoordinate) != null;
            }
        }
        public bool HasLocationToSouth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate - 1) != null;
            }
        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if(!CurrentPlayer.Quests.Any(q=>q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }
    }
}
