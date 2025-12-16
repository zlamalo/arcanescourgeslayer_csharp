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
            Player.PlayerRes.RemoveCardFromCollection(droppedCardUI.CollectionId, droppedCardUI.CardUI.Card.Id);
            Player.PlayerRes.Deck.AddCard(droppedCardUI.CardUI.Card);
        }
    }

    public void OnDeckUpdated(Deck newDeck)
    {
        var children = GetChildren();
        foreach (Node child in children)
        {
            RemoveChild(child);
        }
        foreach (Card card in newDeck.Cards)
        {
            var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
            draggableCardUI.UpdateCard(card, newDeck.Id);
            AddChild(draggableCardUI);
        }

    }
}
