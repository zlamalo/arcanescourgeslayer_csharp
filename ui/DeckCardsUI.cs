using System;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class DeckCardsUI : GridContainer
{
    private PackedScene draggableCardUIScene = GD.Load<PackedScene>("res://ui/DraggableCardUI.tscn");

    private Player Player => GetTree().Root.GetNode<Player>("RootNode/World/Player");

    private Array<Card> currentCards = new();

    public override void _Ready()
    {
        base._Ready();
        EventManager.DeckUpdated += OnDeckUpdated;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.DeckUpdated -= OnDeckUpdated;
    }

    public override bool _CanDropData(Vector2 position, Variant data)
    {
        return data.Obj is DraggableCardUI;
    }

    public override void _DropData(Vector2 position, Variant data)
    {
        if (data.Obj is DraggableCardUI droppedCardUI)
        {
            Player.PlayerRes.RemoveCardFromSet(droppedCardUI.SetId, droppedCardUI.CardIndex);
            Player.PlayerRes.Deck.AddCard(droppedCardUI.CardUI.CurrentCard);
        }
    }

    public void OnDeckUpdated(Deck newDeck)
    {
        var newCards = newDeck.CardsInDeck.Except(currentCards ?? []).ToList();
        foreach (Card card in newCards)
        {
            var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
            draggableCardUI.CardUI.UpdateCard(card);
            AddChild(draggableCardUI);
        }

        var removedCards = (currentCards ?? []).Except(newDeck.CardsInDeck).ToList();
        foreach (Card card in removedCards)
        {
            var cardUIToRemove = GetChildren().OfType<DraggableCardUI>().Where(c => c.CardUI.CurrentCard.Id == card.Id).FirstOrDefault();
            if (cardUIToRemove != null)
            {
                RemoveChild(cardUIToRemove);
            }
        }

        currentCards = newDeck.CardsInDeck.Duplicate();
    }
}
