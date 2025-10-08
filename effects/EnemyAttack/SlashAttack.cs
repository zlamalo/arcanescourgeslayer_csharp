using Godot;

[GlobalClass]
public partial class SlashAttack : EnemyAttack
{
    private PackedScene slashScene = GD.Load<PackedScene>("res://effects/areaEffect/Slash.tscn");

    public override void Attack(BaseEntity attacker, Vector2 directionToTarget)
    {
        var slashInstance = slashScene.Instantiate<Slash>();
        slashInstance.SlashAttack = this;
        slashInstance.Rotation = directionToTarget.Angle();
        slashInstance.GlobalPosition = attacker.GlobalPosition + directionToTarget.Normalized() * 8;
        attacker.GetParent().AddChild(slashInstance);
    }
}