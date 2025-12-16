using Godot;

[GlobalClass]
public partial class Deck : CardCollection
{
    public void AddCard(Card card)
    {
        Cards.Add(card);
        EventManager.DeckUpdated?.Invoke(this);
    }

    public override void RemoveCard(Card card)
    {
        Cards.Remove(card);
        Updated();
    }

    public override void Updated()
    {
        EventManager.DeckUpdated?.Invoke(this);
    }

}
