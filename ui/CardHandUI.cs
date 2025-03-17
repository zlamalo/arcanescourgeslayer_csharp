using Godot;
using System;

public partial class CardHandUI : HBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventManager.CardInHandUpdated += OnCardInHandUpdated;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.CardInHandUpdated -= OnCardInHandUpdated;
	}

	private void OnCardInHandUpdated(int placeInHand, ICard addedCard)
	{
		var cardSprite = GetChild(placeInHand).GetChild<Sprite2D>(0);
		var cardTexture = (Texture2D)GD.Load($"res://cards/{addedCard.CardName}.png");
		cardSprite.Texture = cardTexture;

	}
}
