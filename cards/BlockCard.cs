using Godot;

public class BlockCard : ICard
{
    public string CardName => "BlockCard";

    public BaseEntity CardCaster { get; }

    public BlockCard(BaseEntity owner)
    {
        CardCaster = owner;
    }

    public void Cast()
    {
        GD.Print("blocked");
    }
}