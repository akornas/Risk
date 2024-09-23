using System;
using Zenject;

public class WinLoseProvider : IWinLoseProvider
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly ILogProvider _logProvider;

	private MapTile _attackerTile;
	private MapTile _defenderTile;

	private int AttackerTokens => _attackerTile.Data.Tokens;
	private int DefenderTokens => _defenderTile.Data.Tokens;

	public void Initialize(MapTile attackerTile, MapTile defenderTile)
	{
		_attackerTile = attackerTile;
		_defenderTile = defenderTile;
	}

	public float GetWinChance()
	{
		return (float)AttackerTokens / (AttackerTokens + DefenderTokens) * 100;
	}

	public void HandleAttack()
	{
		var winChance = GetWinChance();
		var dicesCount = _gameplayManager.GameplayData.GameplaySettingsData.Dices;
		int winCounter = 0;

		for (int i = 0; i < dicesCount; i++)
		{
			var diceThrowValue = UnityEngine.Random.Range(0, 100);
			if (diceThrowValue <= GetWinChance())
			{
				winCounter++;
			}
		}

		if (winCounter > MathF.Ceiling(dicesCount / 2f))
		{
			HandleWin();
		}
		else
		{
			HandleLose();
		}
	}

	private void HandleWin()
	{
		var tokensOnAttackerTile = 1;
		var tokensOnDefenderTile = _attackerTile.Data.Tokens + _defenderTile.Data.Tokens - 1;

		_attackerTile.Data.Tokens = tokensOnAttackerTile;
		_defenderTile.Data.Tokens = tokensOnDefenderTile;
		_defenderTile.Data.OwnerPlayerIndex = _attackerTile.Data.OwnerPlayerIndex;

		_logProvider.Log($"Player {_attackerTile.Data.OwnerPlayerIndex + 1} won {tokensOnDefenderTile} tokens");
	}

	private void HandleLose()
	{
		_logProvider.Log($"Player {_attackerTile.Data.OwnerPlayerIndex + 1} lost");

		var tokensOnAttackerTile = (int)MathF.Ceiling(_attackerTile.Data.Tokens / 2f);
		var tokensOnDefenderTile = _defenderTile.Data.Tokens;

		_attackerTile.Data.Tokens = tokensOnAttackerTile;
		_defenderTile.Data.Tokens = tokensOnDefenderTile;

		if (_defenderTile.Data.OwnerPlayerIndex != -1)
		{
			_attackerTile.Data.OwnerPlayerIndex = _defenderTile.Data.OwnerPlayerIndex;
		}
	}
}
