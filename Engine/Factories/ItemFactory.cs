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

            BuildWeapon(1501, "Snake fangs", 0, 0, 2);
            BuildWeapon(1502, "Rat claws", 0, 0, 2);
            BuildWeapon(1503, "Spider bite", 0, 0, 4);

            BuildHealingItem(2000, "Granola Bar", 5, 2);
            BuildConsumable(2001, "Healing Herb", 5);

            BuildMiscItem(3001, "Oats", 1);
            BuildMiscItem(3002, "Honey", 2);
            BuildMiscItem(3003, "Raisins", 2);

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
                                                id, name, price, true);
            weapon.Action = new AttackWithWeapon(weapon, minDmg, maxDmg);
            _standardGameItems.Add(weapon);
        }

        private static void BuildHealingItem(int id,string name, int price,int hitPointsToHeal)
        {
            GameItem item = new GameItem(GameItem.ItemCategory.Consumable,
                id, name, price);
            item.Action = new Heal(item, hitPointsToHeal);
            _standardGameItems.Add(item);
        }

        public static string ItemName(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(
                i => i.ItemTypeID == itemTypeID)?.Name ?? "";
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
        public static GameItem CreateGameItem(string name)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(i => i.Name == name);
            
            if (standardItem != null)
            {
                return standardItem.Clone();
            }
            return null;
        }
    }
}
