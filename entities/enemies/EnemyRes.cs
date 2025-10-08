using Godot;

[GlobalClass]
public partial class EnemyRes : EntityRes
{
    [Export]
    public Texture2D Texture;

    [Export]
    public EnemyAttack Attack;
}