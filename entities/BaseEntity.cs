using System.Drawing;
using Godot;

public abstract partial class BaseEntity : CharacterBody2D
{
    private PackedScene healthBarScene = GD.Load<PackedScene>("res://entities/HealthBar.tscn");

    private HealthBar healthBar;

    public abstract int hp { get; set; }

    public override void _Ready()
    {
        base._Ready();
        healthBar = healthBarScene.Instantiate() as HealthBar;
        this.AddChild(healthBar);
        healthBar.Position = new Vector2(-32, -48);
        healthBar.MaxValue = hp;
        healthBar.Value = hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        DisplayDamageNumber(damage);
        healthBar.Value = hp;
        if (hp <= 0)
        {
            QueueFree();
        }
    }

    public async void DisplayDamageNumber(int damageValue)
    {
        Label number = new()
        {
            Text = damageValue.ToString(),
            LabelSettings = new()
            {
                FontSize = GD.RandRange(18, 25)
            },
        };

        number.Position = GlobalPosition + new Vector2(-number.LabelSettings.FontSize / 2, -60);
        AddChild(number);


        //Create tween animation

        var tween = GetTree().CreateTween();
        tween.SetParallel(true);

        Vector2 newPosition = new Vector2(
            number.GlobalPosition.X + GD.RandRange(-15, 15),
            number.GlobalPosition.Y - GD.RandRange(20, 30)
        );
        tween.TweenProperty(number, "global_position", newPosition, 0.25f);
        tween.TweenProperty(number, "scale", new Vector2(0.5f, 0.5f), 0.25f).SetEase(Tween.EaseType.In).SetDelay(0.3f);

        await ToSignal(tween, "finished");
        number.QueueFree();

    }
}