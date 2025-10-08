using Godot;

[GlobalClass]
public partial class ElementalResistance : Resource
{
    [Export]
    public ElementType ElementType;

    [Export]
    public float Value;
}