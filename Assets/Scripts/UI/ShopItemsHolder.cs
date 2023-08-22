using System;
using Items;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShopItemsHolder : MonoBehaviour
    {
        [HideInInspector] public Shop ThisShop;
        
        [SerializeField] private GameObject ButtonPrefab;
        [SerializeField] private GameObject HatsParent;
        [SerializeField] private GameObject ClothesParent;
        [SerializeField] private TextMeshProUGUI PriceText;

        public void ClearAllItems()
        {
            foreach (Transform child in HatsParent.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (Transform child in ClothesParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddItem(Item item, ItemButton.ButtonAction action)
        {
            var button = Instantiate(ButtonPrefab, item.Type == ItemType.Hat ? HatsParent.transform : ClothesParent.transform);
            var itemButton = button.GetComponent<ItemButton>();

            if (itemButton)
                itemButton.SetProperties(action, item);
        }
        
        public void BuyItem(Item item)
        {
            if (!ThisShop)
                return;
            
            ThisShop.BuyItem(item);
        }

        public void SellItem(Item item)
        {
            ThisShop.SellItem(item);
        }

        public void SetText(ItemButton.ButtonAction action, int price)
        {
            PriceText.text = (action == ItemButton.ButtonAction.Buy ? "Buy" : "Sell") + ": " + price;
        }
    }
}
