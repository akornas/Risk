using UnityEngine;
using Zenject;

public class GameplayManager : MonoBehaviour, IGameplayManager
{
	public event System.Action OnPhaseChangedEvent;
	public event System.Action OnRoundChangedEvent;
	public event System.Action OnPlayerChangedEvent;
	public event System.Action OnEndGameEvent;

	[Inject]
	private readonly IGameplaySettingsManager _gameplaySettingsManager;

	[Inject]
	private readonly ISaveController _saveController;

	[Inject]
	private readonly Factory<SettingUpPhase> _settingUpPhaseFactory;

	[Inject]
	private readonly Factory<TakingOverPhase> _takingOverPhaseFactory;

	[SerializeField]
	private GameplayData _gameplayData;

	private IPhase _currentPhase;

	private GameplaySettingsData GameplaySettingsData => _gameplaySettingsManager.GameplaySettingsData;
	public GameplayData GameplayData => _gameplayData;
	public int CurrentRound => GameplayData.CurrentRound;
	public int CurrentPlayerIndex => GameplayData.CurrentPlayerIndex;
	public IPhase CurrentPhase => _currentPhase;

	[Inject]
	public void Initialize()
	{
		InitializeGameplayData();
		SetPhase(_settingUpPhaseFactory.Create());
	}

	private void InitializeGameplayData()
	{
		_gameplayData = new GameplayData(GameplaySettingsData);

		if (_saveController.GameplayDataFromSave != null)
		{
			_gameplayData.CurrentRound = _saveController.GameplayDataFromSave.CurrentRound;
			_gameplayData.CurrentPlayerIndex = _saveController.GameplayDataFromSave.CurrentPlayerIndex;
		}
	}

	private void SetPhase(IPhase newPhase)
	{
		if (_currentPhase != null)
		{
			_currentPhase.CleanUp();
		}

		_currentPhase = newPhase;
		OnPhaseChangedEvent?.Invoke();
	}

	public void EndTurn()
	{
		bool shouldChangeRound = false;
		HandleCurrentPlayer(ref shouldChangeRound);

		if (shouldChangeRound)
		{
			HandleChangingRound();
		}

		_ = _saveController.SaveData(_gameplayData);
	}

	private void HandleCurrentPlayer(ref bool shouldChangeRound)
	{
		shouldChangeRound = SetNextPlayer();
	}

	private bool SetNextPlayer()
	{
		_gameplayData.CurrentPlayerIndex++;

		if (_gameplayData.CurrentPlayerIndex > GameplaySettingsData.Players - 1)
		{
			_gameplayData.CurrentPlayerIndex = 0;
		}

		OnPlayerChangedEvent?.Invoke();
		return _gameplayData.CurrentPlayerIndex == 0;
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
			//TODO Change for phase queue
			SetPhase(_takingOverPhaseFactory.Create());
		}
	}

	private void HandleEndGame()
	{
		OnEndGameEvent?.Invoke();
	}
}
