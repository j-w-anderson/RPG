using Engine.Factories;
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
        #region Properties
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

        public ItemQuantityCollection Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public List<ItemQuantity> Weapons =>
            Inventory.Where(i => i.Item is Weapon).ToList();
        #endregion
        public Player()
        {
            Inventory = new ItemQuantityCollection();
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddItemToInventory(GameItem item)
        {

            Inventory.Add(item);

            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(int itemID)
        {
            Inventory.Remove(ItemFactory.CreateGameItem(itemID));
            OnPropertyChanged(nameof(Weapons));
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.First(i => i.ItemID == item.ItemID).Quantity < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
