public partial class FireImp : EnemyEntity
{
    private int _hp = 250;
    public override int Hp { get => _hp; set => _hp = value; }
    public override float Speed { get => 1.5f; }

    public override int FireResist => 75;


}
