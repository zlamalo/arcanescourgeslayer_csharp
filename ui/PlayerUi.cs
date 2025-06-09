using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerUi : Control
{
	public PackedScene buffUI = GD.Load<PackedScene>("res://ui/BuffUI.tscn");

	public override void _Ready()
	{
		EventManager.DeckSizeUpdated += OnDeckSizeUpdated;
		EventManager.BuffsUpdated += OnBuffChanged;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.DeckSizeUpdated -= OnDeckSizeUpdated;
		EventManager.BuffsUpdated -= OnBuffChanged;
	}

	private void OnDeckSizeUpdated(int deckSize)
	{
		Label deckSizeLabel = GetNode("BottomUI").GetNode("Deck").GetNode<Label>("DeckSize");
		deckSizeLabel.Text = deckSize.ToString() + "cards in deck";
	}

	private void OnBuffChanged(int buffCount, IBuff buff)
	{
		GD.Print(buff == null);
		var activeBuffUI = GetNode("UpperUI").GetNode("Buffs").GetNodeOrNull(nameof(buff));

		if (buffCount > 0 && activeBuffUI == null)
		{
			var buffNode = buffUI.Instantiate();
			buffNode.Name = nameof(buff);
			buffNode.GetNode<Label>("BuffLabel").Text = buffCount.ToString();
			buffNode.GetNode("BuffIcon").GetNode<Sprite2D>("Icon").Texture = (Texture2D)GD.Load($"res://assets/projectiles/fireball.png");
			GetNode("UpperUI").GetNode("Buffs").AddChild(buffNode);
		}
		else if (buffCount > 0 && activeBuffUI != null)
		{
			activeBuffUI.GetNode<Label>("BuffLabel").Text =
				(activeBuffUI.GetNode<Label>("BuffLabel").Text.ToInt() + buffCount).ToString();
		}
		else
		{
			activeBuffUI?.QueueFree();
		}
	}
}
