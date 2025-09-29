using Godot.Collections;

public interface IDamageEffect
{
    //int BaseDamage { get; }
    //ElementalValues DamageType { get; }

    Array<Damage> Damage { get; }
}