using System;
using UnityEngine;

namespace Characters
{
    public class Shopkeeper : Character
    {
        private bool canInteract;

        [SerializeField] private CanvasGroup InteractionCanvasGroup;

        protected override void Start()
        {
            base.Start();
            
            InteractionCanvasGroup.alpha = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponentInParent<Player.Player>();
            
            if (!canInteract && player)
            {
                canInteract = true;

                player.CanInteract = true;
                player.CurrentShopkeeper = this;

                InteractionCanvasGroup.alpha = 1;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponentInParent<Player.Player>();
            
            if (canInteract && player)
            {
                canInteract = false;
                
                player.CanInteract = false;
                player.CurrentShopkeeper = null;
                
                InteractionCanvasGroup.alpha = 0;
            }
        }
    }
}
