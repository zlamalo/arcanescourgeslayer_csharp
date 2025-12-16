using System;
using System.Linq;
using Godot;
using Godot.Collections;

public abstract partial class CardCollection : Resource
{
    [Export]
    public Array<Card> Cards = [];

    public Guid Id;

    public CardCollection()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Call this when collection updated
    /// In implementation should be emited corresponding signal/event
    /// </summary>
    public abstract void Updated();

    public Card GetCardById(Guid id)
    {
        var card = Cards.Where(x => x?.Id == id).FirstOrDefault();
        if (card == null)
        {
            GD.PrintErr("Card with the given ID not found.");
        }
        return card;
    }

    public abstract void RemoveCard(Card card);
}