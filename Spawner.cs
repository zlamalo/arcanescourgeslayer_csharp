using Godot;
using System;

public partial class Spawner : Node2D
{
    private PackedScene wraithScene = GD.Load<PackedScene>("res://entities/enemies/Wraith.tscn");
    private PackedScene fireImpScene = GD.Load<PackedScene>("res://entities/enemies/FireImp.tscn");



    public override void _Ready()
    {
        Timer timer = new()
        {
            WaitTime = 0.2,
            Autostart = true,
            OneShot = false
        };
        AddChild(timer);

        timer.Timeout += () => SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Node world = GetTree().Root.GetNode("Game/World");

        float radius = 200f;
        float angle = (float)GD.RandRange(0, Math.Tau);

        Random rnd = new();
        if (rnd.Next(0, 2) == 1)
        {
            var wraith = wraithScene.Instantiate<Node2D>();
            wraith.GlobalPosition = GlobalPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            world.AddChild(wraith);
        }
        else
        {
            var fireImp = fireImpScene.Instantiate<Node2D>();
            fireImp.GlobalPosition = GlobalPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            world.AddChild(fireImp);
        }


    }
}
