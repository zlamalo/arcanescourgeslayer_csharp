
using Godot;

[GlobalClass]
public partial class ExplosionBuffSpell : Spell
{
    public override void Cast(BaseEntity caster)
    {
        caster.AddAttackBuff(new ExplosionAttackBuff(caster));
    }
}