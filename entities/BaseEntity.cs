using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public abstract partial class BaseEntity : CharacterBody2D
{
    private PackedScene healthBarScene = GD.Load<PackedScene>("res://entities/HealthBar.tscn");
    private PackedScene effectNumberScene = GD.Load<PackedScene>("res://visualEffects/EffectNumber.tscn");
    private HealthBar healthBar;

    protected double MaxHP;
    protected double HP;
    protected float MovementSpeed;
    protected Array<ElementalResistance> ElementalResistances;

    public abstract EntityRes EntityRes { get; }
    public List<IBuff> attackBuffs = new();

    public override void _Ready()
    {
        base._Ready();
        SetupResValues();

        healthBar = healthBarScene.Instantiate() as HealthBar;
        AddChild(healthBar);
        healthBar.Position = new Vector2(-8, -12);

        healthBar.MaxValue = MaxHP;
        HP = MaxHP;
        healthBar.Value = MaxHP;
    }

    public void TakeDamage(IDamageEffect damageEffect)
    {
        double totalDmg = 0;
        double nonMitigatedDmg = 0;
        if (damageEffect.Damage.Any())
        {
            foreach (Damage damage in damageEffect.Damage)
            {
                ElementType element = damage.ElementType;
                double damageAmount = damage.Value;
                float resist = ElementalResistances?.Where(r => r.ElementType == damage.ElementType).FirstOrDefault()?.Value ?? 0;

                int mitigated = (int)(damageAmount * (1 - resist / 100.0));

                totalDmg += mitigated;
                nonMitigatedDmg += damageAmount;
            }
        }

        HP -= totalDmg;
        var color = Colors.Orange;
        if (totalDmg < nonMitigatedDmg * 0.75)
        {
            color = Colors.Gray;
        }
        if (totalDmg > nonMitigatedDmg * 1.25)
        {
            color = Colors.Red;
        }
        DisplayNumber((int)totalDmg, color);
        healthBar.Value = HP;
        if (HP <= 0)
        {
            QueueFree();
        }
    }

    public void Heal(int healAmount)
    {
        var healingDone = Math.Min(healAmount, MaxHP - HP);
        if (healingDone > 0)
        {
            HP += healingDone;
            healthBar.Value = HP;
            DisplayNumber((int)healingDone, Colors.Green);
        }
    }

    public void OnHitboxEntered(Area2D area)
    {
        var damageSource = area as IDamageEffect;
        TakeDamage(damageSource);
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

    private void SetupResValues()
    {
        MaxHP = EntityRes.MaxHP;
        MovementSpeed = EntityRes.MovementSpeed;
        ElementalResistances = EntityRes.ElementalResistances;
    }
}