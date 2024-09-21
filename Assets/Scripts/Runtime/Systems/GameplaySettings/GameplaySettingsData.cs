using UnityEngine;

[System.Serializable]
public class GameplaySettingsData
{
	[SerializeField]
	[Range(2, 4)]
	private int _players;

	[SerializeField]
	[Range(-1, 100)]
	private int _rounds = -1;

	[SerializeField]
	[Range(1, 100)]
	private int _tokens;

	[SerializeField]
	[Range(2, 20)]
	private int _dices;

	public int Players => _players;
	public int Tokens => _tokens;
	public int Dices => _dices;
	public int Rounds => _rounds;

	public GameplaySettingsData(int players, int rounds, int tokens, int dices)
	{
		_players = players;
		_rounds = rounds;
		_tokens = tokens;
		_dices = dices;
	}

	public GameplaySettingsData(GameplaySettingsData data)
	{
		_players = data.Players;
		_rounds = data.Rounds;
		_tokens = data.Tokens;
		_dices = data.Dices;
	}
}
