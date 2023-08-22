namespace Characters.Player.Commands
{
    public class InventoryCommand : PlayerCommand
    {
        public override void Execute()
        {
            base.Execute();

            player.OpenInventory();
        }

        public void Close()
        {
            player.CloseInventory();
        }
    }
}
