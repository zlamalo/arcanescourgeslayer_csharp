using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public abstract partial class BaseEntity : CharacterBody2D
{
    private PackedScene healthBarScene = GD.Load<PackedScene>("res://entities/HealthBar.tscn");

    private PackedScene effectNumberScene = GD.Load<PackedScene>("res://visualEffects/EffectNumber.tscn");

    private HealthBar healthBar;

    public List<IBuff> attackBuffs = new();

    public abstract int Hp { get; set; }

    public int MaxHp { get; set; }

    public override void _Ready()
    {
        base._Ready();
        healthBar = healthBarScene.Instantiate() as HealthBar;
        AddChild(healthBar);
        healthBar.Position = new Vector2(-8, -12);
        healthBar.MaxValue = Hp;
        MaxHp = Hp;
        healthBar.Value = Hp;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        DisplayNumber(damage, Colors.Orange);
        healthBar.Value = Hp;
        if (Hp <= 0)
        {
            QueueFree();
        }
    }

    public void Heal(int healAmount)
    {
        var healingDone = Math.Min(healAmount, MaxHp - Hp);
        if (healingDone > 0)
        {
            Hp += healingDone;
            healthBar.Value = Hp;
            DisplayNumber(healingDone, Colors.Green);
        }
    }

    public void OnHitboxEntered(Area2D area)
    {
        var damageSource = area as IDamageEffect;
        TakeDamage(damageSource.damage);
    }

    public void AddAttackBuff(IBuff buff)
    {
        attackBuffs.Add(buff);
        EventManager.BuffsUpdated(1, buff);
    }

    public void Clearbuffs()
    {
        attackBuffs.ForEach(b => EventManager.BuffsUpdated(0, b));
        attackBuffs.Clear();
    }

    public void DisplayNumber(int value, Color numberColor)
    {
        var number = effectNumberScene.Instantiate<EffectNumber>();
        var position = Position + new Vector2(-5, -20);
        number.Initialize(position, value, numberColor);
        GetParent().AddChild(number);
    }
}