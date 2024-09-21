using UnityEngine;
using Zenject;

public class TurnEnder : MonoBehaviour
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	public void EndTurn()
	{
		var currentPhase = _gameplayManager.CurrentPhase;

		if (currentPhase.CanEndTurn)
		{
			_gameplayManager.EndTurn();
		}
		else
		{
			currentPhase.NotifyWhyCanNotEndTurn();
		}
	}
}
