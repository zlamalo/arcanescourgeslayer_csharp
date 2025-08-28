using Godot;
using System;

public partial class CardHandUI : HBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventManager.CardInSetUpdated += OnCardInHandUpdated;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.CardInSetUpdated -= OnCardInHandUpdated;
	}

	private void OnCardInHandUpdated(int placeInHand, Card addedCard)
	{
		var cardSprite = GetChild(placeInHand).GetChild<Sprite2D>(0);
		var cardTexture = addedCard.Texture;// (Texture2D)GD.Load($"res://assets/cards/{addedCard.CardName}.png");
		cardSprite.Texture = cardTexture;
	}

	public void OnMouseEntered()
	{
		GetTree().Paused = true;
	}

	public void OnMouseExited()
	{
		GetTree().Paused = false;
	}
}
