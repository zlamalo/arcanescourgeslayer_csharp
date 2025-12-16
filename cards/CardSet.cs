using System.Linq;
using Godot;

[GlobalClass]
public partial class CardSet : CardCollection
{
    public bool Ready;

    public int CooldownMs => Cards.ToList().Select(card => card?.CooldownMs ?? 0).Sum();

    public CardSet()
    {
        Ready = true;
    }

    public void AddCard(Card card, int position)
    {
        if (position < 0 || position > Cards.Count)
        {
            GD.PrintErr("Position out of bounds when adding card to CardSet.");
            return;
        }
        if (Cards[position] != null)
        {
            GD.PrintErr($"Position [{position}] already occupied when adding card to CardSet.");
            return;
        }

        Cards[position] = card;
    }

    public override void Updated()
    {
        EventManager.CardSetUpdated.Invoke(this);
    }

    public override void RemoveCard(Card card)
    {
        var index = Cards.IndexOf(card);
        Cards[index] = null;
        Updated();
    }
}