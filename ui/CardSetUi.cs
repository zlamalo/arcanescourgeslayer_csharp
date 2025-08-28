using Godot;
using System;

public partial class CardSetUi : Control
{
    [Export] public Curve RotationCurve;
    [Export] public Curve HandCurve;



    public PackedScene cardUIScene = GD.Load<PackedScene>("res://ui/CardUI.tscn");

    public void AddCard(Card card)
    {
        var cardUI = cardUIScene.Instantiate();
        cardUI.GetChild<Sprite2D>(0).Texture = card.Texture;// (Texture2D)GD.Load($"res://assets/cards/{card.CardName}.png");
        AddChild(cardUI);
        UpdateCards();
    }

    private void UpdateCards()
    {
        float cardsCount = GetChildCount();

        for (int i = 0; i < cardsCount; i++)
        {
            CardUi card = GetChild<CardUi>(i);
            float yMult = 0;
            float rotationMult = 0;

            if (cardsCount > 1)
            {
                yMult = HandCurve.Sample(1 / (cardsCount - 1) * i);
                rotationMult = RotationCurve.Sample(1 / (cardsCount - 1) * i);
            }

            //var finalX =

            card.Position = new Vector2(i * 15, -10 * yMult);
            card.RotationDegrees = 5 * rotationMult;
        }

    }

}
