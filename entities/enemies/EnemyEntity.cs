using Godot;

public abstract partial class EnemyEntity : BaseEntity
{
    public abstract float Speed { get; }

    CharacterBody2D player;
    public override void _Ready()
    {
        base._Ready();
        player = GetParent().GetNode<CharacterBody2D>("Player");
    }

    public void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = directionToPlayer * Speed;
            MoveAndSlide();
        }
    }
}