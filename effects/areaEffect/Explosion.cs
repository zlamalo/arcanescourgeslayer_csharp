using Godot;
using System;

public partial class Explosion : AreaEffect, IDamageEffect
{
    private AnimationPlayer animationPlayer;

    public int neutralDamage => 0;

    public int fireDamage => 100;

    public int coldDamage => 0;

    public int lightningDamage => 0;

    public int poisonDamage => 0;
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
