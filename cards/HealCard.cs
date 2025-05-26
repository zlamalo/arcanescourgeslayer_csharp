using Godot;

public class HealCard : ICard
{
    public string CardName => "HealCard";

    public BaseEntity CardCaster { get; }

    public HealCard(BaseEntity owner)
    {
        CardCaster = owner;
    }

    public void Cast()
    {
        CardCaster.Heal(10);
    }
}