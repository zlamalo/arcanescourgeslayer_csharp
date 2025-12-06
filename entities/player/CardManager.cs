using Godot;
using System.Threading.Tasks;

public partial class CardManager : Node
{
	public async Task CastSet(BaseEntity caster, CardSet cardSet)
	{
		if (!cardSet.Ready || cardSet.CardsInSet.Count == 0)
			return;

		cardSet.Ready = false;

		foreach (var card in cardSet.CardsInSet)
		{
			if (card != null)
			{
				card.Cast(caster);
				await ToSignal(GetTree().CreateTimer(0.05f, false), SceneTreeTimer.SignalName.Timeout);
			}
		}
		EventManager.SetCasted?.Invoke(cardSet.Id, cardSet.CooldownMs);
		await ToSignal(GetTree().CreateTimer(cardSet.CooldownMs / 1000f, false), SceneTreeTimer.SignalName.Timeout);
		cardSet.Ready = true;
	}
}

