using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GroupedInventoryItem:BaseNotificationClass
    {
        private GameItem _item;

        public GameItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public GroupedInventoryItem(GameItem gameItem, int quantity)
        {
            Quantity = quantity;
            Item = gameItem;

        }

    }
}
