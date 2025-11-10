using Godot;
using System;

public partial class CardSetUI : Control
{
    private PackedScene cardUIScene = GD.Load<PackedScene>("res://ui/CardUI.tscn");

    [Export]
    public Curve RotationCurve;

    [Export]
    public Curve HandCurve;

    public CardSet CurrentCardSet;

    public void UpdateCardSet(CardSet cardSet)
    {
        float cardsCount = GetChildCount();

        for (int i = 0; i < cardSet.CardsInSet.Count; i++)
        {

            Card newCard = cardSet.CardsInSet[i];
            if (cardsCount - i > 0)
            {
                CardUI currentCardUi = GetChild<CardUI>(i);
                if (currentCardUi.CurrentCard != newCard)
                {
                    currentCardUi.UpdateCard(newCard);
                }
            }
            else
            {
                var cardUI = cardUIScene.Instantiate<CardUI>();
                cardUI.UpdateCard(newCard);
                AddChild(cardUI);
            }
        }
        CurrentCardSet = cardSet;
        UpdateCards(); // maybe this should happen only if number of cards changed
    }

    private void UpdateCards()
    {
        float cardsCount = GetChildCount();

        for (int i = 0; i < cardsCount; i++)
        {
            CardUI card = GetChild<CardUI>(i);
            float yMult = 0;
            float rotationMult = 0;

            if (cardsCount > 1)
            {
                yMult = HandCurve.Sample(1 / (cardsCount - 1) * i);
                rotationMult = RotationCurve.Sample(1 / (cardsCount - 1) * i);
            }

            card.Position = new Vector2(i * 15, -10 * yMult);
            card.RotationDegrees = 5 * rotationMult;
        }

    }

}
