using Godot;

public partial class EnemyEntity : BaseEntity
{
    private Timer AttackTimer;

    /// <summary>
    /// Distance in which it will attack player
    /// </summary>
    private int attackRangePx = 20;

    [Export]
    public EnemyRes EnemyRes;
    public CharacterBody2D Player;

    public override EntityRes EntityRes => EnemyRes;

    public override void _Ready()
    {
        base._Ready();
        SetupResValues();

        AttackTimer = GetNode<Timer>("AttackTimer");

        Player = GetParent().GetNode<CharacterBody2D>("Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Player != null && MovementSpeed != 0)
        {
            Vector2 directionToPlayer = Player.GlobalPosition - GlobalPosition;
            if (AttackTimer.TimeLeft == 0 && directionToPlayer.Length() <= attackRangePx)
            {
                EnemyRes.Attack.Attack(this, directionToPlayer);
                AttackTimer.Start();
            }
            else
            {
                Velocity = directionToPlayer.Normalized() * MovementSpeed / 100;
                MoveAndCollide(Velocity);
            }
        }
    }

    private void SetupResValues()
    {
        GetChild<Sprite2D>(0).Texture = EnemyRes.Texture;
    }
}