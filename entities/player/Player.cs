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
		EventManager.CardSetsUpdated?.Invoke(PlayerRes.CardSets);
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

		if (Input.IsActionJustPressed("ui_left_mouse_button"))
		{
			PlayerRes.CardSets[0].PlaySet(this);
		}

		if (Input.IsActionJustPressed("ui_right_mouse_button"))
		{
			PlayerRes.CardSets[1].PlaySet(this);
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
}
