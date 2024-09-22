using System;
using Zenject;

public class TakingOverController : ITakingOverController
{
	public event Action<MapTile> OnSelectedAttackerTileEvent;
	public event Action<MapTile> OnSelectedDefenderTileEvent;
	public event Action OnInitializedEvent;
	public event Action OnRefreshEvent;
	public event Action OnAfterAttackEvent;

	[Inject]
	private readonly IMapController _mapController;

	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly IWinLoseProvider _winLoseProvider;

	[Inject]
	private readonly ILogProvider _logProvider;

	private bool _isEnabled;
	private float _percentageChance;
	private int _tilesAttacked = 0;

	private MapTile _selectedAttackerTile;
	private MapTile _selectedDefenderTile;

	public float PercentageChance => _percentageChance;
	public bool IsAfterAttack { get; private set; }

	public bool IsEnabled
	{
		get => _isEnabled;
		set
		{
			_isEnabled = value;
			HandleIsEnabled();
		}
	}

	private void HandleIsEnabled()
	{
		if (IsEnabled)
		{
			Initialize();
		}
		else
		{
			Deinitialize();
		}
	}

	private void Initialize()
	{
		BindToEvents();
		OnInitializedEvent?.Invoke();
	}

	private void BindToEvents()
	{
		_mapController.OnTileClickedEvent += OnTileClicked;
		_gameplayManager.OnPlayerChangedEvent += OnPlayerChanged;
	}

	private void OnTileClicked(MapTile tile)
	{
		if (!CanAttack())
		{
			return;
		}

		if (_selectedAttackerTile == null)
		{
			HandleSelectingAttackerTile(tile);
		}
		else if (_selectedDefenderTile == null)
		{
			HandleSelectingDefenderrTile(tile);
		}

		CalculatePercentage();
	}

	private bool CanAttack()
	{
		return _tilesAttacked < _gameplayManager.CurrentPhase.TilesLimit;
	}

	private void HandleSelectingAttackerTile(MapTile tile)
	{
		if (tile.Data.OwnerPlayerIndex == _gameplayManager.CurrentPlayerIndex)
		{
			_selectedAttackerTile = tile;
			OnSelectedAttackerTileEvent?.Invoke(_selectedAttackerTile);
		}
	}

	private void HandleSelectingDefenderrTile(MapTile tile)
	{
		if (tile.Data.OwnerPlayerIndex != _gameplayManager.CurrentPlayerIndex && _selectedAttackerTile.IsTileInNeighbour(tile))
		{
			_selectedDefenderTile = tile;
			_winLoseProvider.Initialize(_selectedAttackerTile, _selectedDefenderTile);
			OnSelectedDefenderTileEvent?.Invoke(_selectedDefenderTile);
		}
		else
		{
			_logProvider.Log("You cannot attack that tile");
		}
	}

	private void CalculatePercentage()
	{
		if (_selectedAttackerTile == null || _selectedDefenderTile == null)
		{
			return;
		}

		_percentageChance = _winLoseProvider.GetWinChance();
		OnRefreshEvent?.Invoke();
	}

	private void Deinitialize()
	{
		UnbindFromEvents();
	}

	private void UnbindFromEvents()
	{
		_mapController.OnTileClickedEvent -= OnTileClicked;
		_gameplayManager.OnPlayerChangedEvent -= OnPlayerChanged;
	}

	private void OnPlayerChanged()
	{
		IsAfterAttack = false;
		_tilesAttacked = 0;
	}

	public void Attack()
	{
		_tilesAttacked++;
		_winLoseProvider.HandleAttack();
		CleanUpAfterAttack();
	}

	private void CleanUpAfterAttack()
	{
		IsAfterAttack = true;
		_selectedDefenderTile = null;
		_selectedAttackerTile = null;
		OnAfterAttackEvent?.Invoke();
	}
}
