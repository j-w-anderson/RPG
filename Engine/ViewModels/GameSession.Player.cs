﻿using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public partial class GameSession
    {
        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLevelUp -= OnCurrentPlayerLevelUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed += OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLevelUp += OnCurrentPlayerLevelUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }

        public void UseCurrentConsumable()
        {
            CurrentPlayer.UseCurrentConsumable();
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);
                foreach (ItemQuantity itemQuatity in recipe.OutputItems)
                {
                    GameItem outputItem = ItemFactory.CreateGameItem(itemQuatity.ItemID);
                    RaiseMessage($"You make 1 {outputItem.Name.ToLower()}");
                    CurrentPlayer.AddItemToInventory(outputItem);
                }
            }
            else
            {
                RaiseMessage("You do not have the required ingredients:");
                foreach(ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    RaiseMessage($"  {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
                }
            }
        }

		private void OnCurrentPlayerPerformedAction(object sender,string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentPlayerLevelUp(object sender, System.EventArgs e)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}");
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs e)
        {
            //If player dies, move them to home.
            if (CurrentPlayer.HasAllTheseItems(new List<ItemQuantity> { new ItemQuantity(2001, 1) }))
            {
                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(i => i.ItemTypeID == 2001));
                CurrentPlayer.Heal(10);
                RaiseMessage("You quickly eat a healing herb to gain 10 hit points.");
            }
            else
            {
                RaiseMessage("");
                RaiseMessage($"You have been killed!");

                CurrentLocation = CurrentWorld.LocationAt(0, -1);
                CurrentPlayer.CompletelyHeal();
            }
        }
    }
}
