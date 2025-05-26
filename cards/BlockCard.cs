using Godot;

public class BlockCard : ICard
{
    public string CardName => "BlockCard";

    public CharacterBody2D CardCaster { get; }

    public BlockCard(CharacterBody2D owner)
    {
        CardCaster = owner;
    }

    public void Cast()
    {
        GD.Print("blocked");
    }
}