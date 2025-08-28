using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Deck
{
    private List<Card> cards = new();
    private List<CardSet> cardSets = new();
    private BaseEntity deckOwner;

    public Deck(BaseEntity owner)
    {
        deckOwner = owner;
        LoadStarterDeck();
        for (int i = 0; i < 2; i++)
        {
            cardSets.Add(new CardSet(owner));
            EventManager.SetAdded();
            for (int j = 0; j < 4; j++)
            {
                PutCardInSet(i);
            }
        }
    }

    public void PlaySet(int cardSetNumber)
    {
        cardSets[cardSetNumber].PlaySet();
    }

    private void LoadStarterDeck()
    {
        var FireballCard = GD.Load<Card>("res://cards/FireballCard.tres");
        var BlockCard = GD.Load<Card>("res://cards/BlockCard.tres");
        var ExplosionBuffCard = GD.Load<Card>("res://cards/ExplosionBuffCard.tres");
        var HealCard = GD.Load<Card>("res://cards/HealCard.tres");

        cards.Add((Card)FireballCard.Duplicate(true));
        cards.Add((Card)FireballCard.Duplicate(true));
        cards.Add((Card)FireballCard.Duplicate(true));
        cards.Add((Card)FireballCard.Duplicate(true));

        cards.Add((Card)BlockCard.Duplicate(true));
        cards.Add((Card)ExplosionBuffCard.Duplicate(true));
        cards.Add((Card)ExplosionBuffCard.Duplicate(true));
        cards.Add((Card)ExplosionBuffCard.Duplicate(true));

        cards.Add((Card)HealCard.Duplicate(true));
        cards.Add((Card)HealCard.Duplicate(true));

        // cards.Add(new FireballCard(deckOwner));
        // cards.Add(new FireballCard(deckOwner));
        // cards.Add(new FireballCard(deckOwner));
        // cards.Add(new FireballCard(deckOwner));
        // // cards.Add(new BlockCard(deckOwner));
        // // cards.Add(new BlockCard(deckOwner));
        // // cards.Add(new BlockCard(deckOwner));
        // cards.Add(new HealCard(deckOwner));
        // cards.Add(new HealCard(deckOwner));
        // cards.Add(new ExplosionBuffCard(deckOwner));
        // cards.Add(new ExplosionBuffCard(deckOwner));


    }

    private void PutCardInSet(int setPostition)
    {
        Card cardToPutInSet = DrawCard();
        if (cardToPutInSet != null)
        {
            cardSets[setPostition].AddCard(cardToPutInSet);
            EventManager.CardInSetUpdated?.Invoke(setPostition, cardToPutInSet);
        }
    }

    // private void RemoveCardInHand(int placeInHand)
    // {
    //     cards.Add(cardsInHand[placeInHand]);
    //     cardsInHand[placeInHand] = null;
    // }

    private Card DrawCard()
    {
        Random rnd = new();
        Card card = null;
        if (cards.Count > 0)
        {
            int randomNumber = rnd.Next(0, cards.Count);
            card = cards.ElementAt(randomNumber);
            cards.RemoveAt(randomNumber);
        }
        EventManager.DeckSizeUpdated?.Invoke(cards.Count);
        return card;
    }
}
