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
                DisplayedCardUI.UpdateCard(card, setId, cardIndex);
                return;
            }
            else
            {
                var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
                draggableCardUI.UpdateCard(card, setId, cardIndex);
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
            if (droppedCardUI.SetId == default && droppedCardUI.CardIndex == default)
            {
                // remove from deck
                var deckCard = droppedCardUI.CardUI.CurrentCard;
                Player.PlayerRes.Deck.RemoveCard(droppedCardUI.CardUI.CurrentCard);
                if (DisplayedCardUI == null)
                {
                    // add to new set
                    Player.PlayerRes.AddCardToSet(setId, deckCard, cardIndex, true);
                }
                else
                {
                    // swap with existing card in set
                    var existingCard = DisplayedCardUI.CardUI.CurrentCard;
                    Player.PlayerRes.AddCardToSet(setId, deckCard, cardIndex, true);
                    Player.PlayerRes.Deck.AddCard(existingCard);
                }
            }
            else
            {
                if (DisplayedCardUI == null)
                {
                    // remove from original set
                    Player.PlayerRes.RemoveCardFromSet(droppedCardUI.SetId, droppedCardUI.CardIndex);
                    // add to new set
                    Player.PlayerRes.AddCardToSet(setId, droppedCardUI.CardUI.CurrentCard, cardIndex, true);
                }
                else
                {
                    // swap cards between sets
                    Player.PlayerRes.SwapCardsInSets(droppedCardUI.SetId, droppedCardUI.CardIndex, setId, cardIndex);
                }
            }
        }
    }
}
