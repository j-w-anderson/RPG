using Engine.Actions;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        static ItemFactory()
        {
            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Wooden Club", 10, 1, 3);
            BuildWeapon(1003, "Rusty Sword", 20, 2, 4);
            BuildWeapon(1004, "Soldier's Spear", 30, 2, 5);

            BuildConsumable(2001, "Healing Herb", 5);

            BuildMiscItem(9001, "Snake fang", 1);
            BuildMiscItem(9002, "Snakeskin", 2);
            BuildMiscItem(9003, "Rat tail", 1);
            BuildMiscItem(9004, "Rat fur", 2);
            BuildMiscItem(9005, "Spider fang", 1);
            BuildMiscItem(9006, "Spider silk", 2);
        }

        private static void BuildMiscItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Misc,
                                                id, name, price));
        }

        private static void BuildConsumable(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Consumable,
                                                id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price, int minDmg, int maxDmg)
        {
            GameItem weapon = new GameItem(GameItem.ItemCategory.Weapon,
                                                id, name, price, true );
            weapon.Action = new AttackWithWeapon(weapon, minDmg, maxDmg);
            _standardGameItems.Add(weapon);
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(x => x.ItemTypeID == itemTypeID);
            if (standardItem != null)
            {
                return standardItem.Clone();
            }
            return null;
        }
    }
}
