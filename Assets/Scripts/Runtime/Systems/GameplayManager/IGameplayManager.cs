public interface IGameplayManager
{
	event System.Action OnPhaseChangedEvent;
	event System.Action OnRoundChangedEvent;
	event System.Action OnPlayerChangedEvent;
	event System.Action OnEndGameEvent;

	void EndTurn();
	public GameplayData GameplayData { get; }
	int CurrentRound { get; }
	int CurrentPlayerIndex { get; }
	IPhase CurrentPhase { get; }
}
