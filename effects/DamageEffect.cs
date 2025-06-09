using Godot;

public partial class DamageEffect : Area2D, IDamageEffect
{
    public virtual int neutralDamage { get => 20; }

    public virtual int fireDamage => 0;

    public virtual int coldDamage => 0;

    public virtual int lightningDamage => 0;
    public virtual int poisonDamage => 0;
}