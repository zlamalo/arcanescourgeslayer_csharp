using Godot;
using System;

public partial class EffectNumber : Label
{
    private FontFile font = ResourceLoader.Load<FontFile>("res://assets/PixelFont.TTF");

    private int numberValue;

    private Color numberColor;

    private Vector2 spawnPosition;

    public async override void _Ready()
    {
        Text = numberValue.ToString();
        LabelSettings = new()
        {
            Font = font,
            FontColor = numberColor,
            FontSize = GD.RandRange(8, 12),
            OutlineSize = 5,
            OutlineColor = Colors.Black
        };
        ZIndex = 10;
        GlobalPosition = spawnPosition;

        var tween = GetTree().CreateTween();
        tween.SetParallel(true);

        Vector2 newPosition = new Vector2(
            GlobalPosition.X + GD.RandRange(-7, 7),
            GlobalPosition.Y - GD.RandRange(10, 15)
        );
        tween.TweenProperty(this, "global_position", newPosition, 0.25f);
        tween.TweenProperty(this, "scale", new Vector2(0.5f, 0.5f), 0.25f).SetEase(Tween.EaseType.In).SetDelay(0.3f);

        await ToSignal(tween, "finished");
        QueueFree();
    }

    public void Initialize(Vector2 referencePosition, int numberValue, Color numberColor)
    {
        spawnPosition = referencePosition;
        this.numberValue = numberValue;
        this.numberColor = numberColor;
    }
}
