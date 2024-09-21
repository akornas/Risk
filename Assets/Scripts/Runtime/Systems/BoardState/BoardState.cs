using System.Collections.Generic;
using Zenject;

public class BoardState : IBoardState
{
	public event System.Action OnRefreshBoardStateEvent;

	[Inject]
	private readonly IMapController _mapController;

	private readonly Dictionary<int, int> _playersStates = new();

	public Dictionary<int, int> PlayersStates => _playersStates;

	[Inject]
	public void Initialize()
	{
		foreach (var tile in _mapController.Tiles)
		{
			tile.Data.OnDataChangedEvent += OnDataTileDataChanged;
		}

		CollectPlayerStates();
	}

	private void OnDataTileDataChanged()
	{
		CollectPlayerStates();
	}

	private void CollectPlayerStates()
	{
		_playersStates.Clear();

		foreach (var tile in _mapController.Tiles)
		{
			var tileData = tile.Data;
			var key = tileData.OwnerPlayerIndex;
			var value = tileData.Tokens;

			if (_playersStates.ContainsKey(key))
			{
				_playersStates[key] += value;
			}
			else
			{
				_playersStates.Add(key, value);
			}
		}

		OnRefreshBoardStateEvent?.Invoke();
	}
}
