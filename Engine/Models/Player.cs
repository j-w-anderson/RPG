using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _characterClass;

        public string CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; OnPropertyChanged("CharacterClass"); }
        }
        private int _hitPoints;

        public int HitPoints
        {
            get { return _hitPoints; }
            set { _hitPoints = value; OnPropertyChanged("HitPoints"); }
        }
        private int _experiencePoints;

        public int ExperiencePoints
        {   
            get { return _experiencePoints; }
            set { _experiencePoints = value; OnPropertyChanged("ExperiencePoints"); }
        }

        private int _level;

        public int Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged("Level"); }
        }

        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; OnPropertyChanged("Gold"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
