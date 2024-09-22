using TMPro;
using UnityEngine;
using Zenject;

public class CurrentPlayerUi : MonoBehaviour
{
	[Inject]
	private readonly IGameplayManager _gameplayController;

	[Inject]
	private readonly ITokensController _tokensController;

	[SerializeField]
	private TextMeshProUGUI _playerLabel;

	[SerializeField]
	private TextMeshProUGUI _tokensLabel;

	[Inject]
	public void Initialize()
	{
		_gameplayController.OnPlayerChangedEvent += OnPlayerChanged;
		_tokensController.OnRefreshTokensEvent += OnRefreshTokens;
		_gameplayController.OnPhaseChangedEvent += OnPhaseChanged;
		OnPlayerChanged();
		OnRefreshTokens();
	}

	private void OnPhaseChanged()
	{
		_tokensLabel.gameObject.SetActive(false);
	}

	private void OnRefreshTokens()
	{
		_tokensLabel.text = $"{SettingsController.TOKENS_LABEL} {_tokensController.Tokens}";
	}

	private void OnPlayerChanged()
	{
		_playerLabel.text = $"{SettingsController.PLAYER_LABEL} {_gameplayController.CurrentPlayerIndex + 1}";
	}

	private void OnDestroy()
	{
		_gameplayController.OnPlayerChangedEvent -= OnPlayerChanged;
		_tokensController.OnRefreshTokensEvent -= OnRefreshTokens;
		_gameplayController.OnPhaseChangedEvent -= OnPhaseChanged;
	}
}
