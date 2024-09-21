public interface IWinLoseProvider
{
	void Initialize(MapTileData attackerTile, MapTileData defenderTile);
	float GetWinChance();
	void HandleAttack();
}
