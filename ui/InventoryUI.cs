using Godot;
using Godot.Collections;
using System;

public partial class InventoryUI : Control
{
    private PackedScene cardUIScene = GD.Load<PackedScene>("res://ui/CardUI.tscn");

    private Array<CardSet> cardSets;

    private Deck deck;

    public override void _Ready()
    {
        base._Ready();
        EventManager.CardSetsUpdated += UpdateCardSet;
        EventManager.DeckUpdated += UpdateDeck;

    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.CardSetsUpdated -= UpdateCardSet;
        EventManager.DeckUpdated -= UpdateDeck;
    }

    public void ToggleInventory(bool inventoryOpen)
    {
        if (inventoryOpen)
        {
            FillCardSets();
            FillDeckCards();
        }
        else
        {
            CloseInventory();
        }
    }

    private void UpdateCardSet(Array<CardSet> newCardSets)
    {
        cardSets = newCardSets;
    }

    private void UpdateDeck(Deck newDeck)
    {
        deck = newDeck;
    }

    private void FillCardSets()
    {
        var cardSetsUI = GetNode<VBoxContainer>("LeftSide/CardSets");

        for (int i = 0; i < cardSets.Count; i++)
        {
            var cardContainer = new HBoxContainer();
            cardSetsUI.AddChild(cardContainer);
            foreach (Card card in cardSets[i].CardsInSet)
            {
                var cardUI = cardUIScene.Instantiate<CardUI>();
                cardUI.UpdateCard(card);
                cardContainer.AddChild(cardUI);
            }
        }
    }

    private void CloseInventory()
    {
        var cardSetsUI = GetNode<VBoxContainer>("LeftSide/CardSets");
        foreach (Node child in cardSetsUI.GetChildren())
        {
            child.QueueFree();
        }
        var deckUI = GetNode<GridContainer>("RightSide/DeckCards");
        foreach (Node child in deckUI.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void FillDeckCards()
    {
        var deckUI = GetNode<GridContainer>("RightSide/DeckCards");
        foreach (Card card in deck.CardsInDeck)
        {
            var cardUI = cardUIScene.Instantiate<CardUI>();
            cardUI.UpdateCard(card);
            deckUI.AddChild(cardUI);
        }

    }

}
