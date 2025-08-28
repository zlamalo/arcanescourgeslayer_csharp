
using System;
using System.Collections.Generic;

public static class EventManager
{
    public static Action SetAdded;

    public static Action<int, Card> CardInSetUpdated;

    public static Action<int> DeckSizeUpdated;

    public static Action<int, IBuff> BuffsUpdated;
}
