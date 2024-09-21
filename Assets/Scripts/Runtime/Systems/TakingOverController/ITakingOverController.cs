public interface ITakingOverController : IEnableable
{
	event System.Action OnInitializedEvent;
	event System.Action<MapTileData> OnSelectedAttackerTileEvent;
	event System.Action<MapTileData> OnSelectedDefenderTileEvent;
	event System.Action OnRefreshEvent;
	event System.Action OnAfterAttackEvent;

	float PercentageChance { get; }
	void Attack();
	bool IsAfterAttack { get; }
}
