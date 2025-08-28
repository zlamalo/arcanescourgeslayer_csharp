using System.Collections.Generic;
using Godot;

public class CardSet
{

    private List<Card> cardsInSet = new();

    // private int maxCards;

    //private double cooldown;

    private BaseEntity cardSetOwner;

    public CardSet(BaseEntity cardSetOwner)
    {
        this.cardSetOwner = cardSetOwner;
    }

    public void PlaySet()
    {
        if (cardsInSet.Count > 0)
        {
            cardsInSet.ForEach(c => c.Spell.Cast(cardSetOwner));
        }
    }

    public void AddCard(Card card)
    {
        cardsInSet.Add(card);
    }
}