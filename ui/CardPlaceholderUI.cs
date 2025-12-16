using Godot;
using System;

public partial class CardPlaceholderUI : Panel
{
    private PackedScene draggableCardUIScene = GD.Load<PackedScene>("res://ui/DraggableCardUI.tscn");

    private Player Player => GetTree().Root.GetNode<Player>("RootNode/World/Player");

    private Guid setId;

    private int cardIndex;

    public DraggableCardUI DisplayedCardUI => GetChildOrNull<DraggableCardUI>(0);

    public void DisplayCard(Card card, Guid setId, int cardIndex)
    {
        this.setId = setId;
        this.cardIndex = cardIndex;

        if (card != null)
        {
            if (DisplayedCardUI != null)
            {
                DisplayedCardUI.UpdateCard(card, setId);
                return;
            }
            else
            {
                var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
                draggableCardUI.UpdateCard(card, setId);
                AddChild(draggableCardUI);
            }
        }
        else
        {
            if (DisplayedCardUI != null)
            {
                RemoveChild(DisplayedCardUI);
            }
        }
    }

    public override bool _CanDropData(Vector2 position, Variant data)
    {
        return data.Obj is DraggableCardUI;
    }

    public override void _DropData(Vector2 position, Variant data)
    {
        if (data.Obj is DraggableCardUI droppedCardUI)
        {
            if (DisplayedCardUI != null)
            {
                // swap cards between sets
                Player.PlayerRes.SwapCardsInCollections(droppedCardUI.CollectionId, droppedCardUI.CardUI.Card.Id, setId, DisplayedCardUI.CardUI.Card.Id);
            }
            else
            {
                // remove from original set
                Player.PlayerRes.RemoveCardFromCollection(droppedCardUI.CollectionId, droppedCardUI.CardUI.Card.Id);
                // add to new set
                Player.PlayerRes.AddCardToSet(setId, droppedCardUI.CardUI.Card, cardIndex);
            }
        }
    }
}
