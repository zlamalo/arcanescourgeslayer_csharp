public class ExplosionBuffCard : ICard
{
    public string CardName => "AttackBuffCard";

    public BaseEntity CardCaster { get; }

    public ExplosionBuffCard(BaseEntity owner)
    {
        CardCaster = owner;
    }

    public void Cast()
    {
        CardCaster.AddAttackBuff(new ExplosionAttackBuff(CardCaster));
    }
}