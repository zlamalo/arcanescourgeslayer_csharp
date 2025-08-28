
using Godot;

[GlobalClass]
public partial class HealSpell : Spell
{
    public override void Cast(BaseEntity caster)
    {
        caster.Heal(10);
    }
}
