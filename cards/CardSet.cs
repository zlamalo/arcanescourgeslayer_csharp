using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class CardSet : Resource
{
    public Guid Id;

    public CardSet()
    {
        Id = Guid.NewGuid();
    }

    [Export]
    public Array<Card> CardsInSet;

    public async Task PlaySet(BaseEntity caster)
    {
        if (CardsInSet.Count > 0)
        {
            foreach (var card in CardsInSet)
            {
                if (card != null)
                {
                    card?.Spell.Cast(caster);
                    await Task.Delay(50);
                }
            }
        }
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