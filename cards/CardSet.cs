using System.Collections.Generic;

public class CardSet
{

    private List<ICard> cardsInSet = new();

    // private int maxCards;

    //private double cooldown;

    public void PlaySet()
    {
        if (cardsInSet.Count > 0)
        {
            cardsInSet.ForEach(c => c.Cast());
        }
    }

    public void AddCard(ICard card)
    {
        cardsInSet.Add(card);
    }
}