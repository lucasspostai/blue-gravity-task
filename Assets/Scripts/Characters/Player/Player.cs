using System;
using Characters.Player.Commands;
using Items;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class Player : Character
    {
        private bool isShopOpen;
        private bool isInventoryOpen;
        private Shop shop;

        [HideInInspector] public bool CanInteract;
        [HideInInspector] public Shopkeeper CurrentShopkeeper;
        
        [Header("Commands")]
        [SerializeField] private MovementCommand Movement;
        [SerializeField] private InteractionCommand Interaction;
        [SerializeField] private InventoryCommand Inventory;

        [Header("Physics")] 
        public Rigidbody2D Rigidbody;
        
        [Header("Interface")] 
        public CanvasGroup InventoryInterface;
        public CanvasGroup ShopInterface;
        public HUD HUDInterface;

        public event Action OnOpenInventory;

        private void Awake()
        {
            shop = ShopInterface.GetComponent<Shop>();

            shop.OnBuyItem += BuyItem;
            shop.OnSellItem += SellItem;
        }

        private void OnDisable()
        {
            if(!shop)
                return;
            
            shop.OnBuyItem -= BuyItem;
            shop.OnSellItem -= SellItem;
        }

        protected override void Start()
        {
            base.Start();
            
            InventoryInterface.alpha = 0;
            InventoryInterface.interactable = false;
            InventoryInterface.blocksRaycasts = false;

            ShopInterface.alpha = 0;
            ShopInterface.interactable = false;
            ShopInterface.blocksRaycasts = false;
            
            HUDInterface.Initialize(this);
        }
        
        #region Inputs

        public void MoveInput(InputAction.CallbackContext context)
        {
            if (!Movement || isInventoryOpen || isShopOpen) 
                return;
            
            Movement.MovementValue = context.ReadValue<Vector2>();
            Movement.Execute();
        }
        
        public void InteractInput(InputAction.CallbackContext context)
        {
            if (!Interaction || !CanInteract) 
                return;
            
            if(!isShopOpen)
                Interaction.Execute();
            else
                CloseShop();
        }
        
        public void OpenInventoryInput(InputAction.CallbackContext context)
        {
            if (!Inventory)
                return;
            
            if(!isInventoryOpen)
                Inventory.Execute();
            else
                Inventory.Close();
        }
        
        #endregion

        public void OpenInventory()
        {
            if (isInventoryOpen)
                return;
            
            OnOpenInventory?.Invoke();
            
            InventoryInterface.alpha = 1;
            InventoryInterface.interactable = true;
            InventoryInterface.blocksRaycasts = true;
            isInventoryOpen = true;
        }

        public void CloseInventory()
        {
            if (!isInventoryOpen)
                return;
            
            InventoryInterface.alpha = 0;
            InventoryInterface.interactable = false;
            InventoryInterface.blocksRaycasts = false;
            isInventoryOpen = false;
        }

        public void OpenShop()
        {
            if (!CurrentShopkeeper || isShopOpen)
                return;

            if (shop)
            {
                shop.InitializeShop(
                    InventoryHandler.GetItems(), 
                    CurrentShopkeeper.InventoryHandler.GetItems()
                    );
                
                shop.UpdatePlayerItems(InventoryHandler.GetItems(), InventoryHandler.GetCoins());
            }
            
            ShopInterface.alpha = 1;
            ShopInterface.interactable = true;
            ShopInterface.blocksRaycasts = true;

            isShopOpen = true;
        }

        private void CloseShop()
        {
            if (!isShopOpen)
                return;
            
            ShopInterface.alpha = 0;
            ShopInterface.interactable = false;
            ShopInterface.blocksRaycasts = false;
            
            isShopOpen = false;
        }

        private void BuyItem(Item item)
        {
            if (!InventoryHandler.GetItems().Contains(item) && InventoryHandler.GetCoins() >= item.Price)
            {
                InventoryHandler.SetCoins(-item.Price);
                InventoryHandler.AddItem(item);
                shop.UpdatePlayerItems(InventoryHandler.GetItems(), InventoryHandler.GetCoins());
            }
        }
        
        private void SellItem(Item item)
        {
            if (InventoryHandler.GetItems().Contains(item))
            {
                InventoryHandler.SetCoins(+item.Price);
                InventoryHandler.RemoveItem(item);
                shop.UpdatePlayerItems(InventoryHandler.GetItems(), InventoryHandler.GetCoins());
            }
        }
    }
}
