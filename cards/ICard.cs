using Godot;

public interface ICard
{
    string CardName { get; }
    BaseEntity CardCaster { get; }

    void Cast();
}