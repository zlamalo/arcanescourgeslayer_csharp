using Godot;

[GlobalClass]
public partial class FireballSpell : Spell
{
    private PackedScene fireballScene = GD.Load<PackedScene>("res://effects/projectiles/Fireball.tscn");
    public override void Cast(BaseEntity caster)
    {

        Vector2 direction = (caster.GetGlobalMousePosition() - caster.GlobalPosition).Normalized();

        var fireballInstance = fireballScene.Instantiate() as Fireball;
        fireballInstance.Initialize(caster, direction);

        caster.GetParent().AddChild(fireballInstance);
    }
}
