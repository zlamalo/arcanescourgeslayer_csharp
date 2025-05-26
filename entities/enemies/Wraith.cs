using Godot;
using System;

public partial class Wraith : EnemyEntity
{
    private int _hp = 250;
    public override int Hp { get => _hp; set => _hp = value; }
    public override float Speed { get => 100; }

    private float time = 0;

    public override void _Ready()
    {
        base._Ready();
        var rnd = new Random();
        time = rnd.Next(20);
    }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        // if (Player != null)
        // {
        //     time += (float)delta;

        //     Vector2 directionToPlayer = (Player.GlobalPosition - GlobalPosition).Normalized();
        //     Vector2 perpendicular = new Vector2(-directionToPlayer.Y, directionToPlayer.X);
        //     float sineOffset = Mathf.Sin(time * 10f) * 5f;
        //     var modifiedDir = (directionToPlayer + (perpendicular * sineOffset)).Normalized();
        //     Velocity = modifiedDir * Speed;
        //     MoveAndSlide();
        // }

        FollowPlayer();
    }
}
