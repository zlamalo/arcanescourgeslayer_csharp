using Godot;
using System;

public partial class Wraith : EnemyEntity
{
    private int _hp = 250;
    public override int hp { get => _hp; set => _hp = value; }
    public override float Speed { get => 50; }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        // wip
        //GD.Print(delta);
        //int scale = 50;
        // //Vector2 direction = new Vector2(1, (float)Math.Sin(5 * GlobalPosition.X));//.Normalized();
        // // Vector2 osa = new Vector2(GlobalPosition.X, GlobalPosition.Y).Normalized();

        // float k = directionToPlayer.X / directionToPlayer.Y;
        // Vector2 osa = player.GlobalPosition - GlobalPosition;
        // GD.Print(osa);
        // //float length = (float)Math.Sqrt(GlobalPosition.X * GlobalPosition.X + k * GlobalPosition.Y * k * GlobalPosition.Y);
        // Vector2 direction = new Vector2(
        //     directionToPlayer.X + /*(float)Math.Cos(directionToPlayer.X) **/ scale * (float)Math.Sin(osa.Y),
        //     directionToPlayer.Y + /*(float)Math.Cos(directionToPlayer.Y) **/ scale * (float)Math.Sin(osa.X)).Normalized();

        // Vector2 directionToZero = (-GlobalPosition).Normalized();
        // Vector2 gpNorm = scale * GlobalPosition.Normalized();
        // Vector2 direction2 = new Vector2(
        //     directionToZero.X + /*(float)Math.Cos(directionToPlayer.X) **/  (float)Math.Sin(GlobalPosition.Y),
        //     directionToZero.Y + /*(float)Math.Cos(directionToPlayer.Y) **/  (float)Math.Sin(GlobalPosition.X)).Normalized();
        // GD.Print(direction);

        // Vector2 sinDirection = new Vector2(directionToPlayer.X, (float)Math.Sin(directionToPlayer.X));

        FollowPlayer();
    }
}
