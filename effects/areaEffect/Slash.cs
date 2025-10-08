using Godot;
using Godot.Collections;
using System;

public partial class Slash : Area2D, IDamageEffect
{
    private AnimatedSprite2D animatedSprite;

    public SlashAttack SlashAttack;

    private int hitStartFrame = 2;
    private int hitEndFrame = 5;

    public Array<Damage> Damage => SlashAttack.Damage;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");


        Monitoring = false;
        Monitorable = false;
        PlaySlash();
    }

    public void PlaySlash()
    {
        animatedSprite.Play("slash");
    }

    private void OnFrameChanged()
    {
        int frame = animatedSprite.Frame;

        bool hitActive = frame >= hitStartFrame && frame <= hitEndFrame;
        Monitoring = hitActive;
        Monitorable = hitActive;
    }

    private void OnAnimationFinished()
    {
        QueueFree();
    }
}
