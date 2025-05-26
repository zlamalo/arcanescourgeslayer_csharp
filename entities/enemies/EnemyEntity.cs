using Godot;

public abstract partial class EnemyEntity : BaseEntity
{
    public abstract float Speed { get; }

    public CharacterBody2D Player;
    public override void _Ready()
    {
        base._Ready();
        Player = GetParent().GetNode<CharacterBody2D>("Player");
    }

    public void FollowPlayer()
    {
        if (Player != null)
        {
            Vector2 directionToPlayer = (Player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = directionToPlayer * Speed;
            MoveAndSlide();
        }
    }
}