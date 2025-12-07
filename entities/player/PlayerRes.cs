using System;
using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PlayerRes : EntityRes
{
    [Export]
    public Deck Deck;

    [Export]
    public Array<CardSet> CardSets;

    public CardSet GetCardSetById(Guid id)
    {
        var cardSet = CardSets.Where(x => x.Id == id).FirstOrDefault();
        if (cardSet == null)
        {
            GD.PrintErr("CardSet with the given ID not found.");
        }
        return cardSet;
    }

    public void AddCardToSet(Guid setId, Card card, int position, bool overwrite = false)
    {
        var cardSet = GetCardSetById(setId);
        if (overwrite)
        {
            cardSet.CardsInSet[position] = null;
        }
        cardSet?.AddCard(card, position);
        EventManager.CardSetUpdated.Invoke(cardSet);
    }

    public void RemoveCardFromSet(Guid setId, int position)
    {
        var cardSet = GetCardSetById(setId);
        if (cardSet != null && position >= 0 && position < cardSet.CardsInSet.Count)
        {
            cardSet.CardsInSet[position] = null;
            EventManager.CardSetUpdated.Invoke(cardSet);
        }
        else
        {
            GD.PrintErr("Card not found in the specified CardSet.");
        }
    }

    public void SwapCardsInSets(Guid setIdA, int positionA, Guid setIdB, int positionB)
    {
        var cardSetA = GetCardSetById(setIdA);
        var cardSetB = GetCardSetById(setIdB);

        if (cardSetA == null || cardSetB == null)
        {
            GD.PrintErr("One or both CardSets not found for swapping.");
            return;
        }

        if (positionA < 0 || positionA >= cardSetA.CardsInSet.Count ||
            positionB < 0 || positionB >= cardSetB.CardsInSet.Count)
        {
            GD.PrintErr("One or both positions are out of bounds for swapping.");
            return;
        }

        (cardSetB.CardsInSet[positionB], cardSetA.CardsInSet[positionA]) =
        (cardSetA.CardsInSet[positionA], cardSetB.CardsInSet[positionB]);

        EventManager.CardSetUpdated.Invoke(cardSetA);
        EventManager.CardSetUpdated.Invoke(cardSetB);
    }
}
