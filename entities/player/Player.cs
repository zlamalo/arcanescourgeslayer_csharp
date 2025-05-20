using Godot;
using System;

public partial class Player : BaseEntity
{
	public const float Speed = 200.0f;
	private Deck deck;

	private int _hp = 25;
	public override int hp { get => _hp; set => _hp = value; }

	public override void _Ready()
	{
		base._Ready();
		deck = new(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		if (Input.IsActionPressed("ui_left_mouse_button"))
		{
			deck.PlayCard(0);
		}

		if (Input.IsActionPressed("ui_right_mouse_button"))
		{
			deck.PlayCard(1);
		}

		if (Input.IsActionPressed("ui_Q"))
		{
			deck.PlayCard(2);
		}

		if (Input.IsActionPressed("ui_E"))
		{
			deck.PlayCard(3);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
