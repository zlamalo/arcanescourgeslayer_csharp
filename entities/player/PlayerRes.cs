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
}
