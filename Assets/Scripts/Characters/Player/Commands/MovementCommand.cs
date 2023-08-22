using UnityEngine;

namespace Characters.Player.Commands
{
    public class MovementCommand : PlayerCommand
    {
        private bool isMoving;
        
        [HideInInspector] public Vector2 MovementValue;

        public override void Execute()
        {
            base.Execute();
            
            MovementValue.Normalize();

            isMoving = MovementValue.magnitude > 0f;
            
            if (Mathf.Abs(MovementValue.x) > Mathf.Abs(MovementValue.y))
            {
                MovementValue.y = 0f;
            }
            else
            {
                MovementValue.x = 0f;
            }

            if (isMoving)
            {
                player.SetParameter("FaceX", MovementValue.x);
                player.SetParameter("FaceY", MovementValue.y);
                
                    
            }

            player.PlayAnimation(isMoving ? "Walk" : "Idle");
        }

        private void FixedUpdate()
        {
            if (!player.Rigidbody)
                return;
            
            player.Rigidbody.MovePosition(
                player.Rigidbody.position + 
                MovementValue * (player.Properties.WalkSpeed * Time.fixedDeltaTime));
        }
    }
}
