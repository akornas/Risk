using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayManager : MonoBehaviour, IGameplayManager
{
	public event System.Action OnPhaseChangedEvent;
	public event System.Action OnRoundChangedEvent;
	public event System.Action OnPlayerChangedEvent;

	[Inject]
	private readonly IGameplaySettingsManager _gameplaySettingsManager;

	[Inject]
	private readonly ISaveController _saveController;

	[Inject]
	private readonly Factory<SettingUpPhase> _settingUpPhaseFactory;

	[Inject]
	private readonly Factory<TakingOverPhase> _takingOverPhaseFactory;

	[Inject]
	private readonly ILogProvider _logProvider;

	[Inject]
	private readonly IEndGame _endGameManager;

	[SerializeField]
	private GameplayData _gameplayData;

	private IPhase _currentPhase;
	private List<int> _removedPlayers = new();

	private GameplaySettingsData GameplaySettingsData => _gameplaySettingsManager.GameplaySettingsData;
	public GameplayData GameplayData => _gameplayData;
	public int CurrentRound => GameplayData.CurrentRound;
	public int CurrentPlayerIndex => GameplayData.CurrentPlayerIndex;
	public IPhase CurrentPhase => _currentPhase;

	[Inject]
	public void Initialize()
	{
		InitializeGameplayData();

	}

	private void Start()
	{
		var phase = GetPhaseBasedOnType(_gameplayData.PhaseType);
		SetPhaseFromFactory(phase);
	}

	private void InitializeGameplayData()
	{
		_gameplayData = new GameplayData(GameplaySettingsData);

		TryLoadDataFromSave();
	}

	private void TryLoadDataFromSave()
	{
		if (_saveController.GameplayDataFromSave != null)
		{
			_gameplayData.CurrentRound = _saveController.GameplayDataFromSave.CurrentRound;
			_gameplayData.CurrentPlayerIndex = _saveController.GameplayDataFromSave.CurrentPlayerIndex;
			_gameplayData.TileDatas = _saveController.GameplayDataFromSave.TileDatas;
			_gameplayData.PhaseType = _saveController.GameplayDataFromSave.PhaseType;
		}
	}

	private void SetPhaseFromFactory(IPhase newPhase)
	{
		if (CurrentPhase != null)
		{
			CurrentPhase.CleanUp();
		}

		_gameplayData.PhaseType = newPhase.PhaseType;
		_currentPhase = newPhase;

		OnPhaseChangedEvent?.Invoke();
	}

	public void EndTurn()
	{
		SetNextPlayer(out bool shouldChangeRound);

		if (shouldChangeRound)
		{
			HandleChangingRound();
		}
		_ = _saveController.SaveData(_gameplayData);
	}

	private void SetNextPlayer(out bool shouldChangeRound)
	{
		IterateCurrentPlayer(out shouldChangeRound);
	}

	private void IterateCurrentPlayer(out bool isReturningFirstPlayer)
	{
		isReturningFirstPlayer = false;

		do
		{
			_gameplayData.CurrentPlayerIndex++;

			if (_gameplayData.CurrentPlayerIndex > GameplaySettingsData.Players - 1)
			{
				_gameplayData.CurrentPlayerIndex = 0;
				isReturningFirstPlayer = true;
			}
		} while (_removedPlayers.Contains(_gameplayData.CurrentPlayerIndex));

		OnPlayerChangedEvent?.Invoke();
	}

	private void HandleChangingRound()
	{
		if (_gameplayData.CurrentRound == -1)
		{
			return;
		}

		if (_gameplayData.CurrentRound > 0)
		{
			HandlePhaseEnding();

			_gameplayData.CurrentRound--;
			OnRoundChangedEvent?.Invoke();
		}
		else
		{
			HandleEndGame();
		}
	}

	private void HandlePhaseEnding()
	{
		if (CurrentPhase.CanBeEnded)
		{
			SetPhaseFromFactory(_takingOverPhaseFactory.Create());
		}
	}

	private void HandleEndGame()
	{
		_endGameManager.HandleEndOfRounds();
	}

	private AbstractPhase GetPhaseBasedOnType(GamePhaseType phaseType)
	{
		return phaseType switch
		{
			GamePhaseType.SettingUp => _settingUpPhaseFactory.Create(),
			GamePhaseType.TakingOver => _takingOverPhaseFactory.Create(),
			_ => null,
		};
	}

	public void RemovePlayer(int index)
	{
		if (!_removedPlayers.Contains(index))
		{
			_logProvider.Log($"Player {index + 1} has been defeated");
			_removedPlayers.Add(index);
		}
	}
}
