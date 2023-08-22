using Characters.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        private Player player;
        
        [SerializeField] private TextMeshProUGUI CoinsText;

        public void Initialize(Player inPlayer)
        {
            player = inPlayer;

            player.InventoryHandler.OnCoinsValueUpdated += UpdateCoinsValue;

            CoinsText.text = player.InventoryHandler.GetCoins().ToString();
        }

        private void OnDisable()
        {
            if(!player)
                return;
            
            player.InventoryHandler.OnCoinsValueUpdated -= UpdateCoinsValue;
        }

        private void UpdateCoinsValue(int value)
        {
            CoinsText.text = value.ToString();
        }
    }
}
