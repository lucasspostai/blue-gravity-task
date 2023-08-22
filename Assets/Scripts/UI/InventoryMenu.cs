using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Player;
using Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryMenu : MonoBehaviour
    {
        private Player player;

        [SerializeField] private List<ItemSlotsHolder> SlotsHolders;
        
        [Header("Items")] 
        [SerializeField] private Item EmptyHat;
        [SerializeField] private Item EmptyClothes;
        
        [Header("Images")] 
        [SerializeField] private Image ClothesImage;
        [SerializeField] private Image HatImage;

        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI CoinsText;

        private void Start()
        {
            player = FindObjectOfType<Player>();

            if (player.InventoryInitialized)
                InitializeInventory();
            else
                player.OnSetInventoryItems += InitializeInventory;

            player.OnOpenInventory += InitializeInventory;
        }

        private void OnDisable()
        {
            if(player)
                player.OnSetInventoryItems -= InitializeInventory;
            
            foreach (var slotsHolder in SlotsHolders)
            {
                slotsHolder.OnItemEquipped -= SetEquippedItem;
            }
        }

        private void InitializeInventory()
        {
            foreach (var slotsHolder in SlotsHolders)
            {
                slotsHolder.ClearItems();
            }
            
            GetSlotHolder(ItemType.Hat).AddItem(EmptyHat);
            GetSlotHolder(ItemType.Clothes).AddItem(EmptyClothes);

            foreach (var item in player.InventoryHandler.GetItems())
            {
                GetSlotHolder(item.Type).AddItem(item);
            }

            foreach (var slotsHolder in SlotsHolders)
            {
                slotsHolder.OnItemEquipped += SetEquippedItem;
            }

            GetSlotHolder(ItemType.Hat).EquipItem(player.InventoryHandler.EquippedHat);
            GetSlotHolder(ItemType.Clothes).EquipItem(player.InventoryHandler.EquippedClothes);

            CoinsText.text = player.InventoryHandler.GetCoins().ToString();
        }

        private void SetEquippedItem(Item item)
        {
            if (!item)
                return;
            
            switch (item.Type)
            {
                case ItemType.Hat:
                    
                    if (item.Icon)
                    {
                        HatImage.sprite = item.Icon;
                        HatImage.enabled = true;
                    }
                    else
                    {
                        HatImage.enabled = false;
                    }
                    
                    player.InventoryHandler.EquipHat(item);
                    
                    break;
                case ItemType.Clothes:
                    
                    if (item.Icon)
                    {
                        ClothesImage.sprite = item.Icon;
                        ClothesImage.enabled = true;
                    }
                    else
                    {
                        ClothesImage.enabled = false;
                    }
                    
                    player.InventoryHandler.EquipClothes(item);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private ItemSlotsHolder GetSlotHolder(ItemType type)
        {
            foreach (var slotHolder in SlotsHolders.Where(slotHolder => slotHolder.Type == type))
            {
                return slotHolder;
            }

            return SlotsHolders[0];
        }
    }
}
