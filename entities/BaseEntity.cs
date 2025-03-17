using Godot;

public abstract partial class BaseEntity : Node2D
{
    public abstract int hp { get; set; }

    public override void _Ready()
    {
        base._Ready();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        DisplayDamageNumber(damage);
        if (hp <= 0)
        {
            //QueueFree();
        }
    }

    public async void DisplayDamageNumber(int damageValue)
    {
        GD.Print(this.GlobalPosition);
        Label number = new()
        {
            GlobalPosition = new Vector2(0, -50),
            Text = damageValue.ToString(),
            LabelSettings = new()
            {
                FontSize = GD.RandRange(18, 25)
            }
        };

        AddChild(number);

        number.PivotOffset = number.Size / 2;

        // Create tween animation
        GD.Print("Starting tween");

        var tween = GetTree().CreateTween();
        tween.SetParallel(true);

        Vector2 newPosition = new Vector2(
            number.GlobalPosition.X + GD.RandRange(-15, 15),
            number.GlobalPosition.Y - GD.RandRange(20, 30)
        );

        GD.Print($"Animating to new position: {newPosition}");

        tween.TweenProperty(number, "global_position", newPosition, 0.25f);
        tween.TweenProperty(number, "scale", new Vector2(0.5f, 0.5f), 0.25f).SetEase(Tween.EaseType.In).SetDelay(0.3f);

        GD.Print("Tween animation started");

        await ToSignal(tween, "finished");

        GD.Print("Tween finished, removing label");
        number.QueueFree();

    }
}