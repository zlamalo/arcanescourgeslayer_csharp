using Godot;
using System;

public partial class Dummy : BaseEntity
{
	private int _hp = 2000;
	public override int Hp { get => _hp; set => _hp = value; }

}
