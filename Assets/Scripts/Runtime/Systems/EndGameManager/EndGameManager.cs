using System;
using System.Linq;
using Zenject;

public class EndGameManager : IEndGame, IDisposable
{
	public event System.Action OnEndGameEvent;

	[Inject]
	private readonly IBoardState _boardState;

	[Inject]
	private readonly IGameplayManager _gameplayManager;

	public int LastPlayerIndex { get; private set; }

	[Inject]
	public void OnInjected()
	{
		_boardState.OnRefreshBoardStateEvent += OnRefreshBoardState;
	}

	private void OnRefreshBoardState()
	{
		if (!CanEndGame())
		{
			return;
		}

		CheckRemovePlayer();
		CheckEndGame();
	}

	private void CheckEndGame()
	{
		if (_boardState.PlayersStates.Where(x => x.Key > -1 && x.Value > 0).Count() == 1)
		{
			LastPlayerIndex = _boardState.PlayersStates.Where(x => x.Key > -1 && x.Value > 0).First().Key;
			EndGame();
		}
	}

	private void CheckRemovePlayer()
	{
		var playersCount = _gameplayManager.GameplayData.GameplaySettingsData.Players;

		for (int i = 0; i < playersCount; i++)
		{
			if (!_boardState.PlayersStates.ContainsKey(i))
			{
				_gameplayManager.RemovePlayer(i);
			}
		}
	}

	private bool CanEndGame()
	{
		return _gameplayManager.CurrentPhase != null && _gameplayManager.CurrentPhase.PhaseType != GamePhaseType.SettingUp;
	}

	private void EndGame()
	{
		OnEndGameEvent?.Invoke();
	}

	public void HandleEndOfRounds()
	{
		EndGame();
	}

	public void Dispose()
	{
		_boardState.OnRefreshBoardStateEvent -= OnRefreshBoardState;
	}
}
