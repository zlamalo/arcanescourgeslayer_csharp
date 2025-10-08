using Godot;
using Godot.Collections;

[GlobalClass]
public abstract partial class EnemyAttack : Resource
{
    [Export]
    public Array<Damage> Damage;

    public abstract void Attack(BaseEntity attacker, Vector2 directionToTarget);
}
