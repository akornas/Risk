using System;
using TMPro;
using UnityEngine;
using Zenject;

public class EndGameUi : MonoBehaviour, IDisposable
{
	[Inject]
	private readonly IEndGame _endGameManager;

	[SerializeField]
	private AbstractUiPanel _root;

	[SerializeField]
	private TextMeshProUGUI _textLabel;

	[Inject]
	public void OnInjected()
	{
		_endGameManager.OnEndGameEvent += OnEndGame;
	}

	private void OnEndGame()
	{
		_textLabel.text = $"{SettingsController.PLAYER_LABEL} {_endGameManager.LastPlayerIndex + 1}";
		_ = _root.Open();
	}

	public void Dispose()
	{
		_endGameManager.OnEndGameEvent -= OnEndGame;
	}
}
