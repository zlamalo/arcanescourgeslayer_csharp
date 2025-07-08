using Godot;

public partial class DamageEffect : Area2D, IDamageEffect
{

    public virtual ElementalValues DamageType => new();

    public int BaseDamage => 20;
}