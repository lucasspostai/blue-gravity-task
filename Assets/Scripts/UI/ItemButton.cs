using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ItemButton : MonoBehaviour, IPointerEnterHandler
    {
        public enum ButtonAction
        {
            Equip,
            Buy,
            Sell
        }

        private ButtonAction buttonAction;
        private Item buttonItem;
        private ItemSlotsHolder slotsHolder;
        private ShopItemsHolder shopItemsHolder;
        
        [Header("Images")] 
        [SerializeField] private Image EquippedImage;
        [SerializeField] private Image IconImage;
        
        [SerializeField] private Sprite EmptySlotSprite;

        private void Awake()
        {
            slotsHolder = GetComponentInParent<ItemSlotsHolder>();
            shopItemsHolder = GetComponentInParent<ShopItemsHolder>();
        }
        
        public void SetProperties(ButtonAction action, Item item)
        {
            buttonAction = action;

            if (action != ButtonAction.Equip)
                SetEquipped(false);
            
            buttonItem = item;
            IconImage.sprite = item.Icon ? buttonItem.Icon : EmptySlotSprite;
        }

        public Item GetItem()
        {
            return buttonItem;
        }

        public void SetEquipped(bool equipped)
        {
            EquippedImage.gameObject.SetActive(equipped); 
        }

        public void OnButtonClick()
        {
            switch(buttonAction)
            {
                case ButtonAction.Equip:
                    slotsHolder.EquipItem(buttonItem);
                    break;
                case ButtonAction.Buy:
                    shopItemsHolder.BuyItem(buttonItem);
                    break;
                case ButtonAction.Sell:
                    shopItemsHolder.SellItem(buttonItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buttonAction == ButtonAction.Equip)
                return;
            
            shopItemsHolder.SetText(buttonAction, buttonItem.Price);
        }
    }
}
