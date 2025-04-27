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
        AddChild(healthBar);
        healthBar.Position = new Vector2(-8, -12);
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

    public void OnHitboxEntered(Area2D area)
    {
        var damageSource = area as IDamageEffect;
        TakeDamage(damageSource.damage);
    }

    public async void DisplayDamageNumber(int damageValue)
    {
        Label number = new()
        {
            //GlobalPosition = new Vector2(0, -50),
            Text = damageValue.ToString(),
            LabelSettings = new()
            {
                FontSize = GD.RandRange(8, 12)
            },
        };

        number.GlobalPosition = /*Position +*/ new Vector2(-number.LabelSettings.FontSize / 2, -20);
        AddChild(number);

        //Create tween animation

        var tween = GetTree().CreateTween();
        tween.SetParallel(true);

        Vector2 newPosition = new Vector2(
            number.GlobalPosition.X + GD.RandRange(-7, 7),
            number.GlobalPosition.Y - GD.RandRange(10, 15)
        );
        tween.TweenProperty(number, "global_position", newPosition, 0.25f);
        tween.TweenProperty(number, "scale", new Vector2(0.5f, 0.5f), 0.25f).SetEase(Tween.EaseType.In).SetDelay(0.3f);

        await ToSignal(tween, "finished");
        number.QueueFree();

    }
}