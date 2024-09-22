public interface IPhase
{
	int TilesLimit { get; }
	int Tokens { get; }
	bool CanEndTurn { get; }
	bool CanBeEnded { get; }
	GamePhaseType PhaseType { get; }
	void NotifyWhyCanNotEndTurn();
	void CleanUp();
}
