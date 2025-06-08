using Godot;
using System;

public partial class Explosion : AreaEffect, IDamageEffect
{
    private AnimationPlayer animationPlayer;

    public int damage => 100;


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
