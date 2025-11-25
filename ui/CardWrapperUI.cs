using Godot;
using System;

public partial class CardWrapperUI : Panel, IDropDataControl<CardUI>
{
    private PackedScene cardUIScene = GD.Load<PackedScene>("res://ui/CardUI.tscn");

    private Card displayedCard;

    private Guid representedSetId;

    private int representedCardInSet;

    public void DisplayCard(Card card, Guid setId, int cardIndex)
    {
        displayedCard = card;
        representedSetId = setId;
        representedCardInSet = cardIndex;

        if (card != null)
        {
            var cardUI = cardUIScene.Instantiate<CardUI>();
            cardUI.UpdateCard(card);
            AddChild(cardUI);
        }
    }

    public void RemovePickedData(CardUI data)
    {
        var player = GetTree().Root.GetNode<Player>("RootNode/World/Player");
        player.PlayerRes.RemoveCardFromSet(representedSetId, representedCardInSet);
        RemoveChild(data);
    }

    public override bool _CanDropData(Vector2 position, Variant data)
    {
        return data.Obj is CardUI;
    }

    public override void _DropData(Vector2 position, Variant data)
    {
        if (data.Obj is CardUI card)
        {
            var parent = card.GetParent();
            if (parent is IDropDataControl<CardUI> dropDataControl)
            {
                dropDataControl.RemovePickedData(card);
            }

            AddChild(card);
            card.Position = Vector2.Zero;

            var player = GetTree().Root.GetNode<Player>("RootNode/World/Player");
            player.PlayerRes.AddCardToSet(representedSetId, card.CurrentCard, representedCardInSet);
        }
    }
}
