using TMPro;
using UnityEngine;
using Zenject;

public class CurrentPlayerUi : MonoBehaviour
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly ITokensController _tokensController;

	[Inject]
	private readonly ISettingsController _settingsController;

	[SerializeField]
	private TextMeshProUGUI _playerLabel;

	[SerializeField]
	private TextMeshProUGUI _tokensLabel;

	[Inject]
	public void Initialize()
	{
		_tokensController.OnRefreshTokensEvent += OnRefreshTokens;
		_gameplayManager.OnPlayerChangedEvent += OnPlayerChanged;
		_gameplayManager.OnPhaseChangedEvent += OnPhaseChanged;

		OnPlayerChanged();
		OnRefreshTokens();
	}

	private void OnRefreshTokens()
	{
		_tokensLabel.text = $"{SettingsController.TOKENS_LABEL} {_tokensController.Tokens}";
	}

	private void OnPlayerChanged()
	{
		_playerLabel.color = _settingsController.GetColorForPlayer(_gameplayManager.CurrentPlayerIndex);
		_playerLabel.text = $"{SettingsController.PLAYER_LABEL} {_gameplayManager.CurrentPlayerIndex + 1}";
	}

	private void OnPhaseChanged()
	{
		if (_gameplayManager.CurrentPhase.PhaseType != GamePhaseType.SettingUp)
		{
			_tokensLabel.gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		_tokensController.OnRefreshTokensEvent -= OnRefreshTokens;
		_gameplayManager.OnPlayerChangedEvent -= OnPlayerChanged;
		_gameplayManager.OnPhaseChangedEvent -= OnPhaseChanged;
	}
}
