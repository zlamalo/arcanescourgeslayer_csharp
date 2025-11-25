using System;
using Godot;

[GlobalClass]
public partial class Card : Resource
{
    public Guid Id;

    public Card()
    {
        Id = Guid.NewGuid();
    }

    [Export]
    public string CardName;

    [Export]
    public Texture2D Texture;

    [Export]
    public ElementType ElementType;

    [Export]
    public Spell Spell;
}