using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PlayerRes : EntityRes
{
    [Export]
    public Deck Deck;

    [Export]
    public Array<CardSet> CardSets;
}