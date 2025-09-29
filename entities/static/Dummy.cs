using Godot;
using System;

public partial class Dummy : BaseEntity
{
	private double _hp = 2000;
	public override double Hp { get => _hp; set => _hp = value; }

}
