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
    public class GameSession:INotifyPropertyChanged
    {
        public Player CurrentPlayer { get; set; }
        private Location _currentLocation;

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged("CurrentLocation");
                OnPropertyChanged("HasLocationToNorth");
                OnPropertyChanged("HasLocationToEast");
                OnPropertyChanged("HasLocationToWest");
                OnPropertyChanged("HasLocationToSouth");
            }
        }

        public World CurrentWorld { get; set; }


        public GameSession()
        {
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "James";
            CurrentPlayer.CharacterClass = "Figher";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.ExperiencePoints = 0;
            CurrentPlayer.Level = 1;
            CurrentPlayer.Gold = 10000;
            //            CurrentLocation.ImageName = "/Engine;component/Images/Locations/Home.png";

            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }
        
        public void MoveNorth()
        {
            int xCoordinate = CurrentLocation.XCoordinate;
            int yCoordinate = CurrentLocation.YCoordinate;
            CurrentLocation = CurrentWorld.LocationAt(
                xCoordinate, 
                yCoordinate + 1);
        }
        public void MoveEast()
        {
            int xCoordinate = CurrentLocation.XCoordinate;
            int yCoordinate = CurrentLocation.YCoordinate;
            CurrentLocation = CurrentWorld.LocationAt(
                xCoordinate + 1, 
                yCoordinate);
        }
        public void MoveWest()
        {
            int xCoordinate = CurrentLocation.XCoordinate;
            int yCoordinate = CurrentLocation.YCoordinate;
            CurrentLocation = CurrentWorld.LocationAt(
                xCoordinate - 1, 
                yCoordinate);
        }
        public void MoveSouth()
        {
            int xCoordinate = CurrentLocation.XCoordinate;
            int yCoordinate = CurrentLocation.YCoordinate;
            CurrentLocation = CurrentWorld.LocationAt(
                xCoordinate,
                yCoordinate - 1);
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
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate+1,
                    CurrentLocation.YCoordinate) != null;
            }
        }
        public bool HasLocationToWest
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate-1,
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
