public interface IGameplayManager
{
	event System.Action OnPhaseChangedEvent;
	event System.Action OnRoundChangedEvent;
	event System.Action OnPlayerChangedEvent;
	void EndTurn();
	GameplayData GameplayData { get; }
	void RemovePlayer(int index);
	int CurrentRound { get; }
	int CurrentPlayerIndex { get; }
	IPhase CurrentPhase { get; }
}
