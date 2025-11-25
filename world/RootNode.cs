using Godot;

public partial class RootNode : Node2D
{
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("UI_Inventory"))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        var inventoryNode = GetNode<InventoryUI>("CanvasLayer/InventoryUI");
        inventoryNode.Visible = !inventoryNode.Visible;
        inventoryNode.ToggleInventory(inventoryNode.Visible);
        GetTree().Paused = inventoryNode.Visible;
    }

}
