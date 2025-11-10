using Godot;
using System;

public partial class CardUI : Panel
{

	public Card CurrentCard;


	public void UpdateCard(Card card)
	{
		CurrentCard = card;
		GetNode<Sprite2D>("SpriteWrapper/Sprite2D").Texture = card.Texture;
	}

	public void OnMouseEntered()
	{
		ZIndex = 100;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Hover");
	}

	public void OnMouseExited()
	{
		ZIndex = 0;
		GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("Hover");
	}
}
