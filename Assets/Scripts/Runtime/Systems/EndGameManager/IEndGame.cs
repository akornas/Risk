public interface IEndGame
{
	event System.Action OnEndGameEvent;
	void HandleEndOfRounds();
	int LastPlayerIndex { get; }
}
