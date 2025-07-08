using Godot;
using System;
using System.Collections.Generic;

public partial class Explosion : AreaEffect, IDamageEffect
{
    private AnimationPlayer animationPlayer;

    public ElementalValues DamageType => new(new Dictionary<ElementType, int>
    {
        {ElementType.Fire, 100}
    });

    public int BaseDamage => 0;


    public override void _Ready()
    {
        base._Ready();
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Explosion");
    }

    public void OnAnimationFinished(StringName animationName)
    {
        if (animationName == "Explosion")
        {
            QueueFree();
        }
    }
}
