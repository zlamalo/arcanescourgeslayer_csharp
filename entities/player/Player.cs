using Godot;
using System;

public partial class Player : BaseEntity
{
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback stateMachine;
	private Node2D hands;

	[Export]
	public PlayerRes PlayerRes;

	public override EntityRes EntityRes => PlayerRes;

	public override void _Ready()
	{
		base._Ready();
		animationTree = GetNode<AnimationTree>("AnimationTree");
		stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		stateMachine.Travel("Direction");

		hands = GetNode<Node2D>("Hands");

		EventManager.DeckUpdated?.Invoke(PlayerRes.Deck);
		foreach (var cardSet in PlayerRes.CardSets)
		{
			EventManager.CardSetUpdated?.Invoke(cardSet);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * MovementSpeed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		if (Input.IsActionPressed("ui_left_mouse_button"))
		{
			TryCastSet(0);
		}

		if (Input.IsActionPressed("ui_right_mouse_button"))
		{
			TryCastSet(1);
		}

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

	private void TryCastSet(int index)
	{
		var cardSet = PlayerRes.CardSets[index];
		if (cardSet.Ready)
		{
			_ = GetNode<CardManager>("CardManager").CastSet(this, cardSet);
		}
	}
}
