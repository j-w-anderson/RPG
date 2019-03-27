using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
     public abstract partial class LivingEntity : BaseNotificationClass
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _currentHitPoints;
        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        private int _maximumHitPoints;
        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            protected set
            {
                _maximumHitPoints = value;
                OnPropertyChanged();
            }
        }
        
        private int _level;
        public int Level
        {
            get { return _level; }
            protected set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        private int _gold;
        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        private GameItem _currentWeapon;
        public GameItem CurrentWeapon
        {
            get { return _currentWeapon; }
            set
            {
                if(_currentWeapon!=null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }
                _currentWeapon = value;
                if(_currentWeapon!=null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }

        private GameItem _currentConsumable;
        public GameItem CurrentConsumable
        {
            get { return _currentConsumable; }
            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }
                _currentConsumable = value;
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameItem> Inventory { get;  } =
            new ObservableCollection<GameItem>();
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; } =
            new ObservableCollection<GroupedInventoryItem>();

        public List<GameItem> Weapons =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Weapon).ToList();
        public List<GameItem> Consumables =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Consumable).ToList();

        public bool HasConsumable => Consumables.Any();
        public bool IsDead => CurrentHitPoints <= 0;

        public event EventHandler<string> OnActionPerformed;
        public event EventHandler OnKilled;

        protected LivingEntity(string name, int maximumHitPoints,
            int currentHitPoints,int gold,int level=1)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                }
                GroupedInventory.First(
                    gi => gi.Item.ItemTypeID == item.ItemTypeID)
                    .Quantity ++;
            }
            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(Consumables));
            OnPropertyChanged(nameof(HasConsumable));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            GroupedInventoryItem groupedInventoryItemToRemove =
                item.IsUnique?
                GroupedInventory.FirstOrDefault(gi=>gi.Item==item):
                GroupedInventory.FirstOrDefault
                (gi => gi.Item.ItemTypeID == item.ItemTypeID);

            if(groupedInventoryItemToRemove != null)
            {
                if(groupedInventoryItemToRemove.Quantity==1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else
                {
                    groupedInventoryItemToRemove.Quantity--;
                }
            }

            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(Consumables));
            OnPropertyChanged(nameof(HasConsumable));
        }

        public void RemoveItemsFromInventory(List<ItemQuantity> itemQuantities)
            
        {
            foreach(ItemQuantity itemQuantity in itemQuantities)
            {
                for(int i=0;i<itemQuantity.Quantity;i++)
                {
                    RemoveItemFromInventory(Inventory.First(item =>
                        item.ItemTypeID == itemQuantity.ItemID));
                }
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
        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
