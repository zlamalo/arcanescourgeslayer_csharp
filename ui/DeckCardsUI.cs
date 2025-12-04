using Godot;

public partial class DeckCardsUI : GridContainer, IDragDataControl<DraggableCardUI>
{
    private Player Player => GetTree().Root.GetNode<Player>("RootNode/World/Player");

    public void OnDataDropped(DraggableCardUI draggedData, DraggableCardUI targetData)
    {
        Player.PlayerRes.Deck.RemoveCard(draggedData.CardUI.CurrentCard);
        RemoveChild(draggedData);
    }

    public override bool _CanDropData(Vector2 position, Variant data)
    {
        return data.Obj is DraggableCardUI;
    }

    public override void _DropData(Vector2 position, Variant data)
    {
        if (data.Obj is DraggableCardUI droppedCardUI)
        {
            if (droppedCardUI.GetParent() is IDragDataControl<DraggableCardUI> droppedCardParent)
            {
                droppedCardParent.OnDataDropped(droppedCardUI, null);
            }
            AddChild(droppedCardUI);
            droppedCardUI.Position = Vector2.Zero;

            Player.PlayerRes.Deck.AddCard(droppedCardUI.CardUI.CurrentCard);
        }
    }
}
