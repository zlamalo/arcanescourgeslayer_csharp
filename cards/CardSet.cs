using System;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class CardSet : Resource
{
    [Export]
    public Array<Card> CardsInSet;

    public Guid Id;

    public bool Ready;

    public int CooldownMs => CardsInSet.ToList().Select(card => card?.CooldownMs ?? 0).Sum();

    public CardSet()
    {
        Id = Guid.NewGuid();
        Ready = true;
    }

    public void AddCard(Card card, int position)
    {
        if (position < 0 || position > CardsInSet.Count)
        {
            GD.PrintErr("Position out of bounds when adding card to CardSet.");
            return;
        }
        if (CardsInSet[position] != null)
        {
            GD.PrintErr($"Position [{position}] already occupied when adding card to CardSet.");
            return;
        }

        CardsInSet[position] = card;
    }
}