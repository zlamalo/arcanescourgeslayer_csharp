using Godot;

public interface ICard
{
    string CardName { get; }
    CharacterBody2D CardCaster { get; }

    void Cast();
}