using Godot;

public class BlockCard : Card
{
    public BlockCard(CharacterBody2D owner) : base(owner)
    {
        cardName = "block_card";
    }
}