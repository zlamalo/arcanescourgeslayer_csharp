using System.Collections.Generic;

public partial class FireDamageEffect : DamageEffect
{
    public override ElementalValues DamageType => new(new Dictionary<ElementType, int>
    {
        {ElementType.Fire, 10}
    });
}