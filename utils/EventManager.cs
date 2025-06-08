
using System;
using System.Collections.Generic;

public static class EventManager
{
    public static Action<int, ICard> CardInHandUpdated;

    public static Action<int> DeckSizeUpdated;

    public static Action<int, IBuff> BuffsUpdated;
}
