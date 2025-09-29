using Godot;

[GlobalClass]
public partial class ProjectileSpell : Spell
{
    [Export]
    public Projectile Projectile;

    private PackedScene projectileScene = GD.Load<PackedScene>("res://effects/projectiles/ProjectileBase.tscn");

    public override void Cast(BaseEntity caster)
    {
        var projectileInstance = projectileScene.Instantiate() as ProjectileBase;
        Projectile.Caster = caster;
        projectileInstance.Projectile = Projectile;

        caster.GetParent().AddChild(projectileInstance);
    }
}
