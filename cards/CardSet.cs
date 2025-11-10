using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class CardSet : Resource
{
    [Export]
    public Array<Card> CardsInSet;

    public void PlaySet(BaseEntity caster)
    {
        if (CardsInSet.Count > 0)
        {
            CardsInSet.ToList().ForEach(c => c.Spell.Cast(caster));
        }
    }

    public void AddCard(Card card)
    {
        CardsInSet.Add(card);
    }
}