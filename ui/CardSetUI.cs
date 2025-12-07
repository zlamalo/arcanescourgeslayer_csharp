using Godot;
using System;
using System.Linq;

public partial class CardSetUI : Control
{
    private PackedScene cardUIScene = GD.Load<PackedScene>("res://ui/CardUI.tscn");

    [Export]
    public Curve RotationCurve;

    [Export]
    public Curve HandCurve;

    public CardSet CurrentCardSet;

    public override void _Ready()
    {
        EventManager.SetCasted += OnSetCasted;
    }

    public override void _ExitTree()
    {
        EventManager.SetCasted -= OnSetCasted;
    }

    public void UpdateCardSet(CardSet cardSet)
    {
        float cardsCount = GetChildCount();

        for (int i = 0; i < cardSet.CardsInSet.Count; i++)
        {
            Card newCard = cardSet.CardsInSet[i];
            if (cardsCount - i > 0)
            {
                CardUI currentCardUi = GetChild<CardUI>(i);
                currentCardUi.UpdateCard(newCard);
            }
            else
            {
                var cardUI = cardUIScene.Instantiate<CardUI>();
                cardUI.UpdateCard(newCard);
                AddChild(cardUI);
                RearangeCards(); // new card added, update positions
            }
        }
        CurrentCardSet = cardSet;
    }

    private void RearangeCards()
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

    private void OnSetCasted(Guid setId, int cooldown)
    {
        if (CurrentCardSet != null && CurrentCardSet.Id == setId)
        {
            foreach (CardUI cardUI in GetChildren().Cast<CardUI>())
            {
                cardUI.StartCooldown(cooldown);
            }
        }
    }
}
