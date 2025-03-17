using Godot;
using System;

public class Card
{
	public string cardName;
	protected CharacterBody2D cardCaster;

	public Card(CharacterBody2D owner)
	{
		cardCaster = owner;
	}

	public virtual void Cast()
	{
	}
}
