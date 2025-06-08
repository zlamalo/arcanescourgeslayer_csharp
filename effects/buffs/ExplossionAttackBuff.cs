using Godot;

public class ExplosionAttackBuff : IBuff
{
    private PackedScene explosionScene = GD.Load<PackedScene>("res://effects/areaEffect/Explosion.tscn");

    private BaseEntity buffCaster;

    public ExplosionAttackBuff(BaseEntity caster)
    {
        buffCaster = caster;
    }
    public void CastBuffEffect(Node2D target)
    {
        var explosion = explosionScene.Instantiate<Explosion>();
        explosion.GlobalPosition = target.GlobalPosition;
        buffCaster.GetParent().CallDeferred("add_child", explosion);
    }
}