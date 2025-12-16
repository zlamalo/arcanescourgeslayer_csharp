using System;
using Godot;

public partial class InventoryCardSet : HBoxContainer
{
    private PackedScene cardPlaceholderUIScene = GD.Load<PackedScene>("res://ui/CardPlaceholderUI.tscn");

    private Guid cardSetId;

    public override void _Ready()
    {
        base._Ready();
        EventManager.CardSetUpdated += OnCardSetUpdated;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventManager.CardSetUpdated -= OnCardSetUpdated;
    }

    public void OnCardSetUpdated(CardSet cardSet)
    {
        if (cardSetId != default && cardSetId != cardSet.Id)
            return;

        cardSetId = cardSet.Id;

        var childCount = GetChildCount();
        for (int i = 0; i < cardSet.Cards.Count; i++)
        {
            var card = cardSet.Cards[i];
            if (childCount - i > 0)
            {
                var existingCardUI = GetChild<CardPlaceholderUI>(i);
                existingCardUI.DisplayCard(card, cardSet.Id, i);
            }
            else
            {
                var draggableCardUI = cardPlaceholderUIScene.Instantiate<CardPlaceholderUI>();
                draggableCardUI.DisplayCard(card, cardSet.Id, i);
                AddChild(draggableCardUI);
            }
        }
    }
}