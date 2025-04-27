using Godot;

public partial class Projectile : Area2D, IDamageEffect
{
    protected Vector2 direction;
    private int speed = 300;

    public int damage { get => 20; }

    private int pierce = 2;

    public Projectile()
    {

    }
    public Projectile(CharacterBody2D caster, Vector2 direction)
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
    public void Initialize(CharacterBody2D caster, Vector2 direction)
    {
        GlobalPosition = caster.GlobalPosition;
        this.direction = direction;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Rotation = direction.Angle();
        GlobalPosition += direction * speed * (float)delta;
    }

    public void OnProjectileHit(Area2D area)
    {
        pierce--;
        if (pierce <= 0)
        {
            QueueFree();
        }
    }
}