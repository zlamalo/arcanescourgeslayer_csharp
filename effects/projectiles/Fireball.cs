using Godot;
using System;
using System.Collections.Generic;

public partial class Fireball : Projectile
{
	public override ElementalValues DamageType => new(new Dictionary<ElementType, int>
	{
		{ElementType.Fire, 20}
	});

	public Fireball()
	{

	}
	public Fireball(BaseEntity caster, Vector2 direction) : base(caster, direction)
	{ }
}
