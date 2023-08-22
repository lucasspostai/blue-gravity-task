namespace Characters.Player.Commands
{
    public class InteractionCommand : PlayerCommand
    {
        public override void Execute()
        {
            base.Execute();

            player.OpenShop();
        }
    }
}
