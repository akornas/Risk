public interface ITakingOverController : IEnableable
{
	event System.Action OnInitializedEvent;
	event System.Action<MapTile> OnSelectedAttackerTileEvent;
	event System.Action<MapTile> OnSelectedDefenderTileEvent;
	event System.Action OnRefreshEvent;
	event System.Action OnAfterAttackEvent;

	float PercentageChance { get; }
	void Attack();
	bool IsAfterAttack { get; }
}
