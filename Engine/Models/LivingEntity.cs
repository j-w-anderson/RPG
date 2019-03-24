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
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _currentHitPoints;

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }
        private int _maximumHitPoints;

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }
        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; } =
            new ObservableCollection<GameItem>();

        protected LivingEntity() { }

        public List<Weapon> Weapons =>
            Inventory.OfType<Weapon>().ToList();

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
    }
}