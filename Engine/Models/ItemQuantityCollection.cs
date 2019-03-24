using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ItemQuantityCollection : ObservableCollection<ItemQuantity>
    {
        // Maintains a list of items with their quantities


        new public void Add(ItemQuantity IQ)
        {
            ItemQuantity item = Items.FirstOrDefault(i => i.ItemID == IQ.ItemID);
            if (item == null)
            {
                base.Add(IQ);
            }
            else
            {
                item.Quantity += IQ.Quantity;
            }
        }

        new public void Remove(ItemQuantity IQ)
        {
            ItemQuantity item = Items.FirstOrDefault(i => i.ItemID == IQ.ItemID);
            if (item != null)
            {
                item.Quantity -= IQ.Quantity;
                if(item.Quantity <= 0)
                {
                    base.Remove(item);
                }
            }
        }

        public void Add(GameItem item)
        {
            Add(new ItemQuantity(item.ItemTypeID, 1));
        }

        public void Remove(GameItem item)
        {
            Remove(new ItemQuantity(item.ItemTypeID, 1));
        }



    }
}
