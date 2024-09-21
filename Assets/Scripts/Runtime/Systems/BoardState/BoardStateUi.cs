using TMPro;
using UnityEngine;
using Zenject;

public class BoardStateUi : MonoBehaviour
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly IBoardState _boardState;

	[SerializeField]
	private TextMeshProUGUI[] _playerLabels;

	[Inject]
	public void Initialize()
	{
		BindToEvents();
		SetupLabels();
	}

	private void BindToEvents()
	{
		_boardState.OnRefreshBoardStateEvent += OnRefreshBoardState;
	}

	private void OnRefreshBoardState()
	{
		for (int i = 0; i < _boardState.PlayersStates.Count; i++)
		{
			if (_boardState.PlayersStates.ContainsKey(i))
			{
				_playerLabels[i].text = $"Player {i + 1} - {_boardState.PlayersStates[i]}";
			}
		}
	}

	private void SetupLabels()
	{
		for (int i = 0; i < _gameplayManager.GameplayData.GameplaySettingsData.Players; i++)
		{
			if (_playerLabels[i] != null)
			{
				_playerLabels[i].gameObject.SetActive(true);
			}
		}
	}

	private void OnDestroy()
	{
		_boardState.OnRefreshBoardStateEvent -= OnRefreshBoardState;
	}
}
