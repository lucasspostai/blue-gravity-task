using System;
using Items;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        public CharacterProperties Properties;
        
        [Header("Animation")]
        [SerializeField] private Animator AnimatorController;

        [Header("Items")]
        public CharacterInventory InventoryHandler;
        [SerializeField] private SpriteLibrary EquippedClothes;
        [SerializeField] private SpriteLibrary EquippedHat;
        
        [HideInInspector] public bool InventoryInitialized;
        public event Action OnSetInventoryItems;

        protected virtual void Start()
        {
            if (!InventoryHandler)
                return;

            InventoryHandler.OnEquipClothes += EquipClothes;
            InventoryHandler.OnEquipHat += EquipHat;
            
            InventoryHandler.EquipHat(Properties.EquippedHat);
            InventoryHandler.EquipClothes(Properties.EquippedClothes);
            
            InventoryHandler.SetCoins(Properties.Coins);

            foreach (var item in Properties.InventoryItems)
            {
                InventoryHandler.AddItem(item);
            }

            InventoryInitialized = true;
            
            OnSetInventoryItems?.Invoke();
        }

        private void EquipClothes(Item item)
        {
            if (!item)
                return;

            if (item.LibraryAsset)
            {
                EquippedClothes.spriteLibraryAsset = item.LibraryAsset;
                EquippedClothes.gameObject.SetActive(true);
            }
            else
            {
                EquippedClothes.gameObject.SetActive(false);
            }
        }
        
        private void EquipHat(Item item)
        {
            if (!item)
                return;
            
            if (item.LibraryAsset)
            {
                EquippedHat.spriteLibraryAsset = item.LibraryAsset;
                EquippedHat.gameObject.SetActive(true);
            }
            else
            {
                EquippedHat.gameObject.SetActive(false);
            }
        }

        public void PlayAnimation(string animationName)
        {
            AnimatorController.Play(animationName);
        }

        public void SetParameter(string parameterName, float value)
        {
            AnimatorController.SetFloat(parameterName, value);
        }
    }
}
