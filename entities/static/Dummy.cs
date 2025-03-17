using Godot;
using System;

public partial class Dummy : BaseEntity
{
	private int _hp = 25;
	public override int hp { get => _hp; set => _hp = value; }

	public void OnHitboxEntered(Area2D area)
	{
		var damageSource = area as DamageEffect;
		TakeDamage(damageSource.damage);
	}

}
