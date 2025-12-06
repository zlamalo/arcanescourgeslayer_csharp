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

    public Guid Id;

    public int CooldownMs = 200;

    public Card()
    {
        Id = Guid.NewGuid();
    }

    public void Cast(BaseEntity caster)
    {
        Spell.Cast(caster);
        EventManager.CardCasted?.Invoke(Id);
    }
}