
using System;

public static class EventManager
{
    public static Action<int, ICard> CardInHandUpdated;

    public static Action<int> DeckSizeUpdated;
}
