﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _currentHitPoints;

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }
        private int _maximumHitPoints;

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            private set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }
        private int _gold;

        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; } =
            new ObservableCollection<GameItem>();

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; } =
            new ObservableCollection<GroupedInventoryItem>();


        public List<Weapon> Weapons =>
            Inventory.OfType<Weapon>().ToList();

        public bool IsDead => CurrentHitPoints <= 0;

        public event EventHandler OnKilled;

        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints,int gold)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
        }

        public void TakeDamage(int damage)
        {
            CurrentHitPoints -= damage;

            if(IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;
            if(CurrentHitPoints>MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amount)
        {
            Gold += amount;
        }

        public void SpendGold(int amount)
        {
            if(amount > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amount}.");
            }
            Gold -= amount;
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
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItemToRemove =
                GroupedInventory.FirstOrDefault(gi => gi.Item == item);

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
        }

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }
    }
}
