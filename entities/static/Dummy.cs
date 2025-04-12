using Godot;
using System;

public partial class Dummy : BaseEntity
{
	private int _hp = 200;
	public override int hp { get => _hp; set => _hp = value; }

	// public override void _Ready()
	// {
	// 	var healthBar = GetNode<HealthBar>("HealthBar");
	// 	healthBar.MaxValue = _hp;
	// 	healthBar.Value = _hp;
	// }
	public void OnHitboxEntered(Area2D area)
	{
		var damageSource = area as DamageEffect;
		TakeDamage(damageSource.damage);
		// var healthBar = GetNode<HealthBar>("HealthBar");
		// healthBar.Value = _hp;
	}

}
