using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class Deck : Resource
{
    [Export]
    public Array<Card> CardsInDeck;

    public void AddCard(Card card)
    {
        CardsInDeck.Add(card);
        EventManager.DeckUpdated?.Invoke(this);
    }

    public void RemoveCard(Card card)
    {
        CardsInDeck.Remove(card);
        EventManager.DeckUpdated?.Invoke(this);
    }

    private Card DrawCard()
    {
        Random rnd = new();
        Card card = null;
        if (CardsInDeck.Count > 0)
        {
            int randomNumber = rnd.Next(0, CardsInDeck.Count);
            card = CardsInDeck.ElementAt(randomNumber);
            CardsInDeck.RemoveAt(randomNumber);
        }
        EventManager.DeckUpdated?.Invoke(this);
        return card;
    }
}
