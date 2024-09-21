using System.Collections.Generic;

public interface IBoardState
{
	event System.Action OnRefreshBoardStateEvent;
	Dictionary<int, int> PlayersStates { get; }
}
