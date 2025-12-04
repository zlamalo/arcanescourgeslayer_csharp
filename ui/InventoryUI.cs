using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class InventoryUI : Control
{
    private PackedScene draggableCardUIScene = GD.Load<PackedScene>("res://ui/DraggableCardUI.tscn");
    private PackedScene cardPlaceholderUIScene = GD.Load<PackedScene>("res://ui/CardPlaceholderUI.tscn");

    private Array<CardSet> cardSets = new();

    private Deck deck;

    public override void _Ready()
    {
        base._Ready();
        EventManager.CardSetUpdated += OnCardSetUpdated;
        EventManager.DeckUpdated += UpdateDeck;

    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.CardSetUpdated += OnCardSetUpdated;
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

    private void OnCardSetUpdated(CardSet updatedCardSet)
    {
        if (cardSets.Any(cs => cs.Id == updatedCardSet.Id))
        {
            int index = cardSets.IndexOf(cardSets.Where(cs => cs.Id == updatedCardSet.Id).First());
            cardSets[index] = updatedCardSet;
        }
        else
        {
            cardSets.Add(updatedCardSet);
        }
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
            var cardSet = cardSets[i];
            var cardContainer = new HBoxContainer();
            cardSetsUI.AddChild(cardContainer);
            foreach (Card card in cardSet.CardsInSet)
            {
                var cardWrapperUI = cardPlaceholderUIScene.Instantiate<CardPlaceholderUI>();
                cardWrapperUI.DisplayCard(card, cardSet.Id, cardSet.CardsInSet.IndexOf(card));
                cardContainer.AddChild(cardWrapperUI);

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
            var draggableCardUI = draggableCardUIScene.Instantiate<DraggableCardUI>();
            draggableCardUI.CardUI.UpdateCard(card);
            deckUI.AddChild(draggableCardUI);
        }

    }

}
