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

    private bool inventoryOpen = false;

    public override void _Ready()
    {
        base._Ready();
        EventManager.CardSetUpdated += OnCardSetUpdated;
        //EventManager.DeckUpdated += UpdateDeck;

    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.CardSetUpdated -= OnCardSetUpdated;
        //EventManager.DeckUpdated -= UpdateDeck;
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        Visible = inventoryOpen;
    }

    private void OnCardSetUpdated(CardSet updatedCardSet)
    {
        if (!cardSets.Any(cs => cs.Id == updatedCardSet.Id))
        {
            cardSets.Add(updatedCardSet);

            var cardSetsUI = GetNode<VBoxContainer>("LeftSide/CardSets");
            InventoryCardSet inventoryCardSet = new();
            inventoryCardSet.OnCardSetUpdated(updatedCardSet);
            cardSetsUI.AddChild(inventoryCardSet);
        }
    }
}
