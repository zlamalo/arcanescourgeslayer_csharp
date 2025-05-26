using Godot;
using System;

public partial class Spawner : Node2D
{
    private PackedScene wraithScene = GD.Load<PackedScene>("res://entities/enemies/Wraith.tscn");

    public override void _Ready()
    {
        Timer timer = new()
        {
            WaitTime = 0.2,
            Autostart = true,
            OneShot = false
        };
        AddChild(timer);

        timer.Timeout += () => SpawnWraith();
    }

    private void SpawnWraith()
    {
        Node world = GetTree().Root.GetNode("Game/World");

        float radius = 200f;
        float angle = (float)GD.RandRange(0, Math.Tau);

        var wraith = wraithScene.Instantiate<Node2D>();
        wraith.GlobalPosition = GlobalPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

        world.AddChild(wraith);
    }
}
