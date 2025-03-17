using Godot;
using System;

public partial class Fireball : Projectile
{

	public Fireball()
	{

	}
	public Fireball(CharacterBody2D caster, Vector2 direction) : base(caster, direction)
	{ }
}
