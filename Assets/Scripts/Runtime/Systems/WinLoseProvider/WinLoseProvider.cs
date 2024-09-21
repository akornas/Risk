using System;
using Zenject;

public class WinLoseProvider : IWinLoseProvider
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	private MapTileData _attackerTile;
	private MapTileData _defenderTile;

	private int AttackerTokens => _attackerTile.Tokens;
	private int DefenderTokens => _defenderTile.Tokens;


	public void Initialize(MapTileData attackerTile, MapTileData defenderTile)
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

		if (winCounter > MathF.Ceiling(dicesCount / 2))
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
		var tokensOnDefenderTile = _attackerTile.Tokens + _defenderTile.Tokens - 1;

		_attackerTile.Tokens = tokensOnAttackerTile;
		_defenderTile.Tokens = tokensOnDefenderTile;
		_defenderTile.OwnerPlayerIndex = _attackerTile.OwnerPlayerIndex;
	}

	private void HandleLose()
	{
		var tokensOnAttackerTile = (int)MathF.Ceiling(_attackerTile.Tokens / 2);
		var tokensOnDefenderTile = _defenderTile.Tokens;

		_attackerTile.Tokens = tokensOnAttackerTile;
		_defenderTile.Tokens = tokensOnDefenderTile;
		_attackerTile.OwnerPlayerIndex = _defenderTile.OwnerPlayerIndex;
	}

}
