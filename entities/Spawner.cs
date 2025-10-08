using Godot;
using Godot.Collections;
using System;

public partial class Spawner : Node2D
{
    private PackedScene enemyBaseScene = GD.Load<PackedScene>("res://entities/enemies/EnemyBase.tscn");
    private Node worldNode;

    [Export]
    public Array<EnemyRes> SpawnableEnemies;

    [Export]
    public double SpawnRate;

    public override void _Ready()
    {
        worldNode = GetTree().Root.GetNode("Game/World");
        Timer timer = new()
        {
            WaitTime = SpawnRate,
            Autostart = true,
            OneShot = false
        };
        AddChild(timer);

        timer.Timeout += () => SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        float radius = 200f;
        float angle = (float)GD.RandRange(0, Math.Tau);

        SpawnableEnemies.PickRandom();
        EnemyEntity enemy = enemyBaseScene.Instantiate() as EnemyEntity;
        enemy.EnemyRes = SpawnableEnemies.PickRandom();
        enemy.GlobalPosition = GlobalPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        worldNode.AddChild(enemy);
    }
}
