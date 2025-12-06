using Godot;
using System;

public partial class CardUI : Panel
{
	private AnimationPlayer cardAnimationPlayer;
	private AnimationPlayer cooldownAnimationPlayer;

	public Card CurrentCard { get; private set; }

	public override void _Ready()
	{
		base._Ready();
		EventManager.CardCasted += OnCardCasted;

		cardAnimationPlayer = GetNode<AnimationPlayer>("CardAnimationPlayer");
		cooldownAnimationPlayer = GetNode<AnimationPlayer>("SpriteWrapper/CooldownOverlay/CooldownAnimationPlayer");
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
			GetNode<Sprite2D>("SpriteWrapper/GlowOverlay").Texture = card?.Texture;

			var cooldownProgressBar = GetNode<TextureProgressBar>("SpriteWrapper/CooldownOverlay/CooldownProgressBar");
			cooldownProgressBar.TextureProgress = card?.Texture;
			cooldownProgressBar.Visible = false;

			if (cooldownAnimationPlayer?.IsPlaying() == true)
			{
				SetAnimationProgress();
			}
		}
	}

	public void OnCardCasted(Guid cardId)
	{
		if (CurrentCard?.Id == cardId)
		{
			cardAnimationPlayer.Play("CardCasted");
		}
	}

	public void StartCooldown(int cooldownMs)
	{
		cooldownAnimationPlayer.SpeedScale = 1000f / cooldownMs;
		cooldownAnimationPlayer.Play("Cooldown");
	}

	/// <summary>
	/// Sets the cooldown animation progress based on elapsed time.
	/// This is needed when the card UI is updated while a cooldown is in progress.
	/// </summary>
	private void SetAnimationProgress()
	{
		double animPos = cooldownAnimationPlayer.CurrentAnimationPosition;
		cooldownAnimationPlayer.Seek(animPos, true);
	}

	public void OnMouseEntered()
	{
		ZIndex = 100;
		cardAnimationPlayer.Play("Hover");
	}

	public void OnMouseExited()
	{
		ZIndex = 0;
		cardAnimationPlayer.PlayBackwards("Hover");
	}
}
