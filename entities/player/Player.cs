using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 500.0f;
	private Deck deck;

	public override void _Ready()
	{
		base._Ready();
		deck = new(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		if (Input.IsActionJustPressed("ui_left_mouse_button"))
		{
			deck.PlayCard(0);
		}

		if (Input.IsActionJustPressed("ui_right_mouse_button"))
		{
			deck.PlayCard(1);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
