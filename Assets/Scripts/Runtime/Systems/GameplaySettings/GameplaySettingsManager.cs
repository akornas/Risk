using UnityEngine;

public class GameplaySettingsManager : MonoBehaviour, IGameplaySettingsManager
{
	private GameplaySettingsData _gameplaySettingsData;

	public GameplaySettingsData GameplaySettingsData => _gameplaySettingsData;

	public void CreateSettingsData(int players, int rounds, int tokens, int dices)
	{
		_gameplaySettingsData = new(players, rounds, tokens, dices);
	}

	public void CreateSettingsData(GameplaySettingsData _data)
	{
		_gameplaySettingsData = new(_data);
	}
}
