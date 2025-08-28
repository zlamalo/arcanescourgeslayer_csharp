using Godot;

[GlobalClass]
public abstract partial class Spell : Resource
{
    public abstract void Cast(BaseEntity caster);
}
