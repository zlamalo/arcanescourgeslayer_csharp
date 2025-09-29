using Godot;
using Godot.Collections;

public partial class DamageEffect : Area2D, IDamageEffect
{
    [Export]
    public Array<Damage> DamageValues;

    public Array<Damage> Damage => DamageValues;
}