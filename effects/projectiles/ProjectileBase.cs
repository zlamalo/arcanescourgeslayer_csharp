using Godot;
using Godot.Collections;
using System;

public partial class ProjectileBase : Area2D, IDamageEffect
{
    //[Export]
    public Projectile Projectile;

    private int speed;
    private int pierce;
    private BaseEntity caster;
    private Vector2 direction;

    public Array<Damage> Damage => Projectile.Damage;


    public override void _Ready()
    {
        base._Ready();
        // setup stats
        var projectileSprite = GetChild<Sprite2D>(0);
        projectileSprite.Texture = Projectile.Texture;
        speed = Projectile.Speed;
        pierce = Projectile.Pierce;

        caster = Projectile.Caster;

        // setup physics values
        GlobalPosition = caster.GlobalPosition;
        direction = (caster.GetGlobalMousePosition() - caster.GlobalPosition).Normalized();

        // connect on exited
        var notifier = new VisibleOnScreenNotifier2D();
        AddChild(notifier);
        notifier.Connect("screen_exited", new Callable(this, nameof(OnScreenExited)));
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Rotation = direction.Angle();
        GlobalPosition += direction * speed * (float)delta;
    }

    public void OnProjectileHit(Area2D area)
    {
        var buffs = caster.attackBuffs;
        if (buffs.Count > 0)
        {
            buffs.ForEach(x => x.CastBuffEffect(this));
            caster.Clearbuffs();
        }
        pierce--;
        if (pierce <= 0)
        {
            QueueFree();
        }
    }

    private void OnScreenExited()
    {
        QueueFree();
    }
}
