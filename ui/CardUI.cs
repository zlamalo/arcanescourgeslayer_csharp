using Godot;
using System;

public partial class CardUI : Panel
{
	public Card CurrentCard { get; private set; }

	public override void _Ready()
	{
		base._Ready();
		EventManager.CardCasted += OnCardCasted;

	}

	public override void _ExitTree()
	{
		base._ExitTree();
		EventManager.CardCasted -= OnCardCasted;
	}

	public void UpdateCard(Card card)
	{
		if (CurrentCard?.Id != card?.Id)
		{
			CurrentCard = card;
			GetNode<Sprite2D>("SpriteWrapper/Sprite2D").Texture = card?.Texture;
			GetNode<Sprite2D>("SpriteWrapper/Sprite2D/GlowOverlay").Texture = card?.Texture;
		}
	}

	public void OnCardCasted(Guid cardId)
	{
		if (CurrentCard?.Id == cardId)
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("CardCasted");
		}
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
