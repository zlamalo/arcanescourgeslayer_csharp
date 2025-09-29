using System.Collections.Generic;

public partial class FireImp : EnemyEntity
{
    private double _hp = 250;
    public override double Hp { get => _hp; set => _hp = value; }
    public override float Speed { get => 1.5f; }

    public override ElementalValues Resistances => new(new Dictionary<ElementType, int>
    {
        {ElementType.Fire, 75},
    });
}
