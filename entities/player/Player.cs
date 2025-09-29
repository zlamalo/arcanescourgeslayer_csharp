using Godot;
using System;
using System.Collections.Generic;

public partial class Player : BaseEntity
{
	private AnimationTree animationTree;

	private AnimationNodeStateMachinePlayback stateMachine;

	private Node2D hands;

	public const float Speed = 200.0f;
	private Deck deck;

	private double _hp = 100;
	public override double Hp { get => _hp; set => _hp = value; }

	public override ElementalValues Resistances => new(new Dictionary<ElementType, int>
	{
		{ElementType.Fire, -80}
	});


	public override void _Ready()
	{
		base._Ready();
		deck = new(this);
		animationTree = GetNode<AnimationTree>("AnimationTree");
		stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		stateMachine.Travel("Direction");

		hands = GetNode<Node2D>("Hands");
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

		if (Input.IsActionJustPressed("ui_left_mouse_button"))
		{
			deck.PlaySet(0);
		}

		if (Input.IsActionJustPressed("ui_right_mouse_button"))
		{
			deck.PlaySet(1);
		}

		// if (Input.IsActionJustPressed("ui_Q"))
		// {
		// 	deck.PlaySet(2);
		// }

		// if (Input.IsActionJustPressed("ui_E"))
		// {
		// 	deck.PlaySet(3);
		// }

		Vector2 mousePos = GetLocalMousePosition();
		Vector2 dir = mousePos.Normalized();
		animationTree.Set("parameters/Direction/blend_position", dir);
		var globalMousePosition = GetGlobalMousePosition();
		hands.LookAt(globalMousePosition);
		var angle = GetAngleTo(globalMousePosition);
		var pi = Math.PI;
		if (angle < -pi / 4 || angle > 3 * pi / 4)
		{
			hands.GetNode<Sprite2D>("RightHand").ZIndex = -1;
		}
		else
		{
			hands.GetNode<Sprite2D>("RightHand").ZIndex = 1;
		}
		if (angle < pi / 4 && angle > -3 * pi / 4)
		{
			hands.GetNode<Sprite2D>("LeftHand").ZIndex = -1;
		}
		else
		{
			hands.GetNode<Sprite2D>("LeftHand").ZIndex = 1;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
