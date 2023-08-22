using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class CharacterInventory : MonoBehaviour
    {
        private List<Item> inventoryItems;
        private int coins;
        
        [HideInInspector] public Item EquippedHat;
        [HideInInspector] public Item EquippedClothes;

        public event Action<Item> OnEquipClothes;
        public event Action<Item> OnEquipHat;
        public event Action<int> OnCoinsValueUpdated;

        private void Awake()
        {
            inventoryItems = new List<Item>();
        }

        public void EquipHat(Item hat)
        {
            EquippedHat = hat;
            OnEquipHat?.Invoke(hat);
        }

        public void EquipClothes(Item clothes)
        {
            EquippedClothes = clothes;
            OnEquipClothes?.Invoke(clothes);
        }

        public void AddItem(Item item)
        {
            inventoryItems.Add(item);
        }
        
        public void RemoveItem(Item item)
        {
            if(inventoryItems.Contains(item))
                inventoryItems.Remove(item);
        }

        public List<Item> GetItems()
        {
            return inventoryItems;
        }

        public int GetCoins()
        {
            return coins;
        }

        public void SetCoins(int value)
        {
            coins += value;
            OnCoinsValueUpdated?.Invoke(coins);
        }
    }
}
