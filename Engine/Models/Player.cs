using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : BaseNotificationClass
    {

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string _characterClass;

        public string CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; OnPropertyChanged(nameof(CharacterClass)); }
        }
        private int _hitPoints;

        public int HitPoints
        {
            get { return _hitPoints; }
            set { _hitPoints = value; OnPropertyChanged(nameof(HitPoints)); }
        }
        private int _experiencePoints;

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; OnPropertyChanged(nameof(ExperiencePoints)); }
        }

        private int _level;

        public int Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged(nameof(Level)); }
        }

        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; OnPropertyChanged(nameof(Gold)); }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public List<GameItem> Weapons =>
            Inventory.Where(i => i is Weapon).ToList();

        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            OnPropertyChanged(nameof(Weapons));
        }
    }
}
