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
                GetMonsterAtLocation();
            }
        }
        public World CurrentWorld { get; set; }

        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate + 1) != null;
        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate + 1,
                CurrentLocation.YCoordinate) != null;
        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate - 1,
                CurrentLocation.YCoordinate) != null;
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate - 1) != null;

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
    }
}
