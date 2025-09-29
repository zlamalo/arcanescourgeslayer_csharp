using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Projectile : Resource
{
    [Export]
    public Texture2D Texture;

    [Export]
    public int Speed;

    [Export]
    public int Pierce;

    [Export]
    public Array<Damage> Damage;

    public BaseEntity Caster;

}