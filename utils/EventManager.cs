
using System;

public static class EventManager
{
    public static Action<CardSet> CardSetUpdated;

    public static Action<Deck> DeckUpdated;

    public static Action<int, IBuff> BuffsUpdated;
}
