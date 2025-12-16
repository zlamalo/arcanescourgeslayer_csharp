using System;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class PlayerUI : Control
{
	public PackedScene buffUI = GD.Load<PackedScene>("res://ui/BuffUI.tscn");
	public PackedScene cardSetUIScene = GD.Load<PackedScene>("res://ui/CardSetUI.tscn");

	public override void _Ready()
	{
		EventManager.BuffsUpdated += OnBuffChanged;
		EventManager.DeckUpdated += OnDeckUpdated;
		EventManager.CardSetUpdated += OnCardSetUpdated;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.BuffsUpdated -= OnBuffChanged;
		EventManager.DeckUpdated -= OnDeckUpdated;
		EventManager.CardSetUpdated -= OnCardSetUpdated;
	}

	private void OnDeckUpdated(Deck deck)
	{
		Label deckSizeLabel = GetNode("BottomUI").GetNode("Deck").GetNode<Label>("DeckSize");
		deckSizeLabel.Text = deck.Cards.Count + "cards in deck";
	}

	private void OnBuffChanged(int buffCount, IBuff buff)
	{
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

	private void OnCardSetUpdated(CardSet cardSet)
	{
		var children = GetNode("BottomUI").GetNode("CardSets").GetChildren();
		var currentCardSetUI = children.Where(c => c.GetType() == typeof(CardSetUI))
			.Cast<CardSetUI>()
			.FirstOrDefault(cs => cs.CurrentCardSet.Id == cardSet.Id);

		if (currentCardSetUI != null)
		{
			currentCardSetUI.UpdateCardSet(cardSet);
		}
		else
		{
			CardSetUI cardSetUI = cardSetUIScene.Instantiate<CardSetUI>();
			cardSetUI.UpdateCardSet(cardSet);
			GetNode("BottomUI").GetNode("CardSets").AddChild(cardSetUI);
		}
		UpdateCardSetsPositions();
	}

	private void UpdateCardSetsPositions()
	{
		var bottomUi = GetNode<PanelContainer>("BottomUI");
		var cardSets = GetNode("BottomUI").GetNode<Control>("CardSets");

		var sets = cardSets.GetChildren();
		var numberOfSets = sets.Count;
		var gap = bottomUi.Size.X / (numberOfSets + 1);
		for (int i = numberOfSets - 1; i >= 0; i--)
		{
			CardSetUI set = (CardSetUI)sets[i];
			set.OffsetLeft = i * gap;
		}
	}
}
