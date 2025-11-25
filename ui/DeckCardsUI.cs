using Godot;
using System;

public partial class DeckCardsUI : GridContainer, IDropDataControl<CardUI>
{
    public void RemovePickedData(CardUI data)
    {
        var player = GetTree().Root.GetNode<Player>("RootNode/World/Player");
        player.PlayerRes.Deck.RemoveCard(data.CurrentCard);
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
            player.PlayerRes.Deck.AddCard(card.CurrentCard);
        }
    }
}
