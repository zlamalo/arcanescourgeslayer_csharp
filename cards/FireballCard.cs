
using Godot;

public class FireballCard : ICard
{
    private PackedScene fireballScene = GD.Load<PackedScene>("res://effects/projectiles/Fireball.tscn");

    public string CardName => "fireball_card";

    public CharacterBody2D CardCaster { get; }

    public FireballCard(CharacterBody2D owner)
    {
        CardCaster = owner;
    }

    public void Cast()
    {
        Vector2 direction = (CardCaster.GetGlobalMousePosition() - CardCaster.GlobalPosition).Normalized();

        var fireballInstance = fireballScene.Instantiate() as Fireball;
        fireballInstance.Initialize(CardCaster, direction);

        CardCaster.GetParent().AddChild(fireballInstance);
    }
}