using Godot;
using System;

public partial class Fireball : Projectile
{
	public override int fireDamage => 20;


	public Fireball()
	{

	}
	public Fireball(BaseEntity caster, Vector2 direction) : base(caster, direction)
	{ }
}
