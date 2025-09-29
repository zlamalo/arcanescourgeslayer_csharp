using Godot;
using Godot.Collections;

public partial class Explosion : AreaEffect, IDamageEffect
{
    private AnimationPlayer animationPlayer;


    public Array<Damage> Damage => new Array<Damage>() { new Damage() { ElementType = ElementType.Fire, Value = 100 } };

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
