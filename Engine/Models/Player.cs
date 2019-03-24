using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties
        private string _characterClass;

        public string CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; OnPropertyChanged(nameof(CharacterClass)); }
        }
        private int _experiencePoints;

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
                SetLevelAndMaximumHitPoints();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }

        #endregion

        public event EventHandler OnLevelUp;

        public Player(string name, string characterClass,
            int experiencePoints, int maximumHitPoints,
            int currentHitPoints, int gold):
            base(name,maximumHitPoints,currentHitPoints,gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Quests = new ObservableCollection<QuestStatus>();
        }
        
        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int orignalLevel = Level;
            Level = (ExperiencePoints / 100) + 1;
            if (Level!=orignalLevel)
            {
                MaximumHitPoints = Level * 10;
                OnLevelUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
