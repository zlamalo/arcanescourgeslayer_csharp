using System;
using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PlayerRes : EntityRes
{
    private Array<CardCollection> CardCollections => [.. CardSets.Cast<CardCollection>().Concat([Deck])];

    [Export]
    public Deck Deck;

    [Export]
    public Array<CardSet> CardSets;

    public T GetCardCollectionById<T>(Guid id) where T : CardCollection
    {
        var cardCollection = CardCollections.FirstOrDefault(x => x.Id == id);
        if (cardCollection is T typedCollection)
        {
            return typedCollection;
        }
        GD.PrintErr("CardCollection with the given ID not found.");
        return null;
    }

    public CardCollection GetCardCollectionById(Guid id)
    {
        return GetCardCollectionById<CardCollection>(id);
    }

    public void AddCardToSet(Guid setId, Card card, int position)
    {
        var cardSet = GetCardCollectionById<CardSet>(setId);
        if (cardSet != null)
        {
            cardSet.AddCard(card, position);
            cardSet.Updated();
        }
    }

    public void RemoveCardFromCollection(Guid collectionId, Guid cardId)
    {
        var collection = GetCardCollectionById(collectionId);
        var card = collection?.GetCardById(cardId);
        if (card != null)
        {
            collection.RemoveCard(card);
        }
        else
        {
            GD.PrintErr($"Card not found in the specified CardCollection of type: {collection.GetType}.");
        }
    }

    public void SwapCardsInCollections(Guid collectionIdA, Guid cardIdA, Guid collectionIdB, Guid cardIdB)
    {
        var cardCollectionA = GetCardCollectionById(collectionIdA);
        var cardCollectionB = GetCardCollectionById(collectionIdB);

        if (cardCollectionA == null || cardCollectionB == null)
        {
            GD.PrintErr("One or both CardCollections not found for swapping.");
            return;
        }

        var cardA = cardCollectionA.GetCardById(cardIdA);
        var cardB = cardCollectionB.GetCardById(cardIdB);

        var indexA = cardCollectionA.Cards.IndexOf(cardA);
        var indexB = cardCollectionB.Cards.IndexOf(cardB);

        if (indexA == -1 || indexB == -1)
        {
            GD.PrintErr("One or both Cards not found for swapping.");
            return;
        }

        (cardCollectionB.Cards[indexB], cardCollectionA.Cards[indexA]) =
        (cardCollectionA.Cards[indexA], cardCollectionB.Cards[indexB]);

        cardCollectionA.Updated();
        cardCollectionB.Updated();
    }
}
