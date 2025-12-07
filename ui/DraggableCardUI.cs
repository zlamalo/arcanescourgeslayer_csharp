using System;
using Godot;

/// <summary>
/// Wrapper UI for cards that can be dragged
/// </summary>
public partial class DraggableCardUI : Control
{
    public CardUI CardUI => GetNode<CardUI>("CardUI");

    public Guid SetId;
    public int CardIndex;

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var preview = Duplicate() as Control;
        SetDragPreview(preview);
        return this;
    }

    public void UpdateCard(Card card, Guid setId, int cardIndex)
    {
        SetId = setId;
        CardIndex = cardIndex;
        CardUI.UpdateCard(card);
    }
}
