using System;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopItemsHolder PlayerItemsHolder;
        [SerializeField] private ShopItemsHolder ShopkeeperItemsHolder;
        [SerializeField] private TextMeshProUGUI CoinsText;

        public event Action<Item> OnBuyItem;
        public event Action<Item> OnSellItem;
        
        public void InitializeShop(List<Item> playerItems, List<Item> shopkeeperItems)
        {
            PlayerItemsHolder.ThisShop = this;
            ShopkeeperItemsHolder.ThisShop = this;
            
            AddItemsToHolder(playerItems, PlayerItemsHolder, ItemButton.ButtonAction.Sell);
            AddItemsToHolder(shopkeeperItems, ShopkeeperItemsHolder, ItemButton.ButtonAction.Buy);
        }

        private void AddItemsToHolder(List<Item> items, ShopItemsHolder holder, ItemButton.ButtonAction action)
        {
            holder.ClearAllItems();
            
            foreach (var item in items)
            {
                holder.AddItem(item, action);
            }
        }

        public void BuyItem(Item item)
        {
            OnBuyItem?.Invoke(item);
        }
        
        public void SellItem(Item item)
        {
            OnSellItem?.Invoke(item);
        }

        public void UpdatePlayerItems(List<Item> playerItems, int coins)
        {
            AddItemsToHolder(playerItems, PlayerItemsHolder, ItemButton.ButtonAction.Sell);
            CoinsText.text = coins.ToString();
        }
    }
}
