using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Deck
{
    private List<ICard> cards = new List<ICard>();
    private int handSize = 4;
    private List<ICard> cardsInHand = new List<ICard>();
    private CharacterBody2D deckOwner;

    public Deck(CharacterBody2D owner)
    {
        deckOwner = owner;
        LoadStarterDeck();
        for (int i = 0; i < handSize; i++)
        {
            cardsInHand.Add(null);
            PutCardInHand(i);
        }
    }

    public void PlayCard(int placeInHand)
    {
        ICard cardToPlay = cardsInHand[placeInHand];
        cardToPlay.Cast();
    }

    private void LoadStarterDeck()
    {
        cards.Add(new FireballCard(deckOwner));
        cards.Add(new FireballCard(deckOwner));
        cards.Add(new FireballCard(deckOwner));
        cards.Add(new FireballCard(deckOwner));
        cards.Add(new FireballCard(deckOwner));
        // cards.Add(new BlockCard(deckOwner));
        // cards.Add(new BlockCard(deckOwner));
        // cards.Add(new BlockCard(deckOwner));
    }

    private void PutCardInHand(int placeInHand)
    {
        ICard cardToPutInHand = DrawCard();
        if (cardToPutInHand != null)
        {
            cardsInHand[placeInHand] = cardToPutInHand;
            EventManager.CardInHandUpdated?.Invoke(placeInHand, cardToPutInHand);
        }
    }

    private ICard DrawCard()
    {
        Random rnd = new();
        ICard card = null;
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
