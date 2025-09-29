using Godot;

[GlobalClass]
public partial class Damage : Resource
{
    [Export]
    public ElementType ElementType;

    [Export]
    public double Value;
}