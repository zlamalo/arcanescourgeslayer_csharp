using Godot;
using Godot.Collections;

[GlobalClass]
public partial class EntityRes : Resource
{
    [Export]
    public float MovementSpeed;

    [Export]
    public double MaxHP;

    [Export]
    public Array<ElementalResistance> ElementalResistances;
}