using Godot;
using System;

public partial class PlayerUi : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventManager.DeckSizeUpdated += OnDeckSizeUpdated;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.DeckSizeUpdated -= OnDeckSizeUpdated;
	}

	private void OnDeckSizeUpdated(int deckSize)
	{
		Label deckSizeLabel = GetNode("BottomUI").GetNode("Deck").GetNode<Label>("DeckSize");
		deckSizeLabel.Text = deckSize.ToString() + "cards in deck";
	}

}
