using System;
using Items;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ItemSlotsHolder : MonoBehaviour
    {
        [SerializeField] private string Name;
        
        [Header("References")] 
        [SerializeField] private TextMeshProUGUI CategoryText;
        [SerializeField] private GameObject ButtonsParent;
        [SerializeField] private GameObject ButtonPrefab;
        
        public ItemType Type;
        
        public event Action<Item> OnItemEquipped;

        private void Start()
        {
            CategoryText.text = Name;
        }

        public void ClearItems()
        {
            foreach (Transform child in ButtonsParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddItem(Item item)
        {
            var button = Instantiate(ButtonPrefab,ButtonsParent.transform);
            var itemButton = button.GetComponent<ItemButton>();

            if (itemButton)
                itemButton.SetProperties(ItemButton.ButtonAction.Equip, item);
        }

        public void EquipItem(Item item)
        {
            foreach (Transform child in ButtonsParent.transform)
            {
                var itemButton = child.GetComponent<ItemButton>();

                child.GetComponent<ItemButton>().SetEquipped(itemButton.GetItem() == item);
            }
            
            OnItemEquipped?.Invoke(item);
        }
    }
}
