using System.Collections.Generic;

public enum ElementType
{
    Fire,
    Cold,
    Lightning,
    Poison
}

public struct ElementalValues
{
    private readonly Dictionary<ElementType, int> _values;

    public ElementalValues()
    {
        _values = new Dictionary<ElementType, int>();
    }

    public ElementalValues(Dictionary<ElementType, int> values)
    {
        _values = values;
    }

    public int this[ElementType type] => _values.TryGetValue(type, out var val) ? val : 0;

    public IEnumerable<ElementType> Types => _values.Keys;

    public Dictionary<ElementType, int> RawValues => _values;
}