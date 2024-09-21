using System;
using Zenject;

public class TakingOverController : ITakingOverController
{
	public event Action<MapTileData> OnSelectedAttackerTileEvent;
	public event Action<MapTileData> OnSelectedDefenderTileEvent;
	public event Action OnInitializedEvent;
	public event Action OnRefreshEvent;
	public event Action OnAfterAttackEvent;

	[Inject]
	private readonly IMapController _mapController;

	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly IWinLoseProvider _winLoseProvider;

	private bool _isEnabled;
	private float _percentageChance;
	private int _tilesAttacked = 0;

	private MapTileData _selectedAttackerTile;
	private MapTileData _selectedDefenderTile;

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

	private void OnTileClicked(MapTileData tileData)
	{
		if (!CanAttack())
		{
			return;
		}

		if (_selectedAttackerTile == null)
		{
			HandleSelectingAttackerTile(tileData);
		}
		else if (_selectedDefenderTile == null)
		{
			HandleSelectingDefenderrTile(tileData);
		}

		CalculatePercentage();
	}

	private bool CanAttack()
	{
		return _tilesAttacked < _gameplayManager.CurrentPhase.TilesLimit;
	}

	private void HandleSelectingAttackerTile(MapTileData tileData)
	{
		if (tileData.OwnerPlayerIndex == _gameplayManager.CurrentPlayerIndex)
		{
			_selectedAttackerTile = tileData;
			OnSelectedAttackerTileEvent?.Invoke(_selectedAttackerTile);
		}
	}

	private void HandleSelectingDefenderrTile(MapTileData tileData)
	{
		if (tileData.OwnerPlayerIndex != _gameplayManager.CurrentPlayerIndex)
		{
			_selectedDefenderTile = tileData;
			_winLoseProvider.Initialize(_selectedAttackerTile, _selectedDefenderTile);
			OnSelectedDefenderTileEvent?.Invoke(_selectedDefenderTile);
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
		//TODO move to WinLoseProvider

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
