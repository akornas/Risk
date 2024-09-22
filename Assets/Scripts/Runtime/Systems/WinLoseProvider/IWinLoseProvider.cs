public interface IWinLoseProvider
{
	void Initialize(MapTile attackerTile, MapTile defenderTile);
	float GetWinChance();
	void HandleAttack();
}
