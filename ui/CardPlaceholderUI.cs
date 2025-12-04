using Godot;
using System;

public partial class CardPlaceholderUI : Panel, IDragDataControl<DraggableCardUI>
{
    private PackedScene draggableCardUIScene = GD.Load<PackedScene>("res://ui/DraggableCardUI.tscn");

    private Player Player => GetTree().Root.GetNode<Player>("RootNode/World/Player");

    private Guid representedSetId;

    private int cardIndex;

    public DraggableCardUI DisplayedCardUI => GetChildOrNull<DraggableCardUI>(0);

    public void DisplayCard(Card card, Guid setId, int cardIndex)
    {
        representedSetId = setId;
        this.cardIndex = cardIndex;

        if (card != null)
        {
            var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
            draggableCardUI.CardUI.UpdateCard(card);
            AddChild(draggableCardUI);
        }
    }

    /// <summary>
    /// Must be called when data from this control is dropped onto another control
    /// </summary>
    /// <param name="draggedData">Is the same object as DisplayedCardUI</param>
    /// <param name="targetData"></param>
    public void OnDataDropped(DraggableCardUI draggedData, DraggableCardUI targetData)
    {
        if (targetData == null)
        {
            Player.PlayerRes.RemoveCardFromSet(representedSetId, cardIndex);
            RemoveChild(draggedData);
        }
        else
        {
            Player.PlayerRes.RemoveCardFromSet(representedSetId, cardIndex);
            Player.PlayerRes.AddCardToSet(representedSetId, targetData.CardUI.CurrentCard, cardIndex);
            draggedData.CardUI.UpdateCard(targetData.CardUI.CurrentCard);
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
            var droppedCard = droppedCardUI.CardUI.CurrentCard; // copy of card because droppedCardUI is modified in OnDataDropped
            if (droppedCardUI.GetParent() is IDragDataControl<DraggableCardUI> droppedCardParent)
            {
                droppedCardParent.OnDataDropped(droppedCardUI, DisplayedCardUI);
            }

            Player.PlayerRes.RemoveCardFromSet(representedSetId, cardIndex);
            Player.PlayerRes.AddCardToSet(representedSetId, droppedCard, cardIndex);

            droppedCardUI.Position = Vector2.Zero;

            if (DisplayedCardUI != null)
            {
                DisplayedCardUI?.CardUI.UpdateCard(droppedCard);
            }
            else
            {
                AddChild(droppedCardUI);
            }
        }
    }
}
