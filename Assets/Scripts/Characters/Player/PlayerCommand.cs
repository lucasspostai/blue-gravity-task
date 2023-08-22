using System;

namespace Characters.Player
{
    public class PlayerCommand : Command
    {
        protected Player player;
        
        public static event Action InputDown;

        protected virtual void Awake()
        {
            player = GetComponentInParent<Player>();
        }

        public override void Execute()
        {
            base.Execute();

            OnInputDown();
        }

        private static void OnInputDown()
        {
            InputDown?.Invoke();
        }
    }
}
