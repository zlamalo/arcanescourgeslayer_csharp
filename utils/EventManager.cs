
using System;
using Godot.Collections;

public static class EventManager
{
    public static Action<Array<CardSet>> CardSetsUpdated;

    public static Action<Deck> DeckUpdated;

    public static Action<int, IBuff> BuffsUpdated;
}
