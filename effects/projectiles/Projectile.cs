using Godot;

public partial class Projectile : Area2D, IDamageEffect
{
    protected Vector2 direction;
    private int speed = 300;

    private int pierce = 2;

    public virtual ElementalValues DamageType => new();

    public int BaseDamage => 0;

    private BaseEntity caster;

    public Projectile()
    {

    }
    public Projectile(BaseEntity caster, Vector2 direction)
    {
        GlobalPosition = caster.GlobalPosition;
        this.direction = direction;
    }

    /// <summary>
    /// Toto je třeba zavolat pokaždé když dělám .Instantiate()
    /// Chtěl jsem to předávat parametricky konstrukotrem, ale to mi zatím nejde
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="direction"></param>
    public void Initialize(BaseEntity caster, Vector2 direction)
    {
        GlobalPosition = caster.GlobalPosition;
        this.direction = direction;
        this.caster = caster;
    }

    public override void _Ready()
    {
        base._Ready();

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