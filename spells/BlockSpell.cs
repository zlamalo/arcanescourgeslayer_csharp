using Godot;

[GlobalClass]
public partial class BlockSpell : Spell
{
    public override void Cast(BaseEntity caster)
    {
        GD.Print("blocked");
    }
}
