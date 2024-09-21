using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayData
{
	[SerializeField]
	private GameplaySettingsData _gameplaySettingsData;

	[SerializeField]
	private int _currentPlayerIndex;

	[SerializeField]
	private int _currentRound;

	[SerializeField]
	private int _currentTokens;

	[SerializeField]
	private List<PlayerData> _playerDatas;

	public int CurrentRound
	{
		get => _currentRound;
		set
		{
			_currentRound = value;
		}
	}

	public int CurrentPlayerIndex
	{
		get => _currentPlayerIndex;
		set
		{
			_currentPlayerIndex = value;
		}
	}

	public int CurrentTokens
	{
		get => _currentTokens;
		set
		{
			_currentTokens = value;
		}
	}

	public GameplaySettingsData GameplaySettingsData => _gameplaySettingsData;

	public GameplayData(GameplaySettingsData gameplaySettingsData)
	{
		_gameplaySettingsData = gameplaySettingsData;
		CurrentRound = _gameplaySettingsData.Rounds;
		CurrentPlayerIndex = 0;
	}
}

[System.Serializable]
public class PlayerData
{
	[SerializeField]
	private int _playerIndex;

	[SerializeField]
	private int _tokensOnFields;
}
