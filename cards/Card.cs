using System;
using Godot;

[GlobalClass]
public partial class Card : Resource
{
    [Export]
    public string CardName;

    [Export]
    public Texture2D Texture;

    [Export]
    public ElementType ElementType;

    [Export]
    public Spell Spell;
}