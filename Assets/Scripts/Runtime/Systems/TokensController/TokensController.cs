using Zenject;

public class TokensController : ITokensController
{
	public event System.Action OnRefreshTokensEvent;

	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly IMapController _mapController;

	private int _takenTiles = 0;
	private bool _isEnabled;

	public int Tokens
	{
		get => _gameplayManager.GameplayData.CurrentTokens;
		set
		{
			_gameplayManager.GameplayData.CurrentTokens = value;
		}
	}

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
	}

	private void BindToEvents()
	{
		_gameplayManager.OnPhaseChangedEvent += OnPhaseChanged;
		_mapController.OnTileClickedEvent += OnTileClicked;
		_gameplayManager.OnPlayerChangedEvent += OnPlayerChanged;
	}

	private void OnPhaseChanged()
	{
		RefreshTokens();
	}

	private void RefreshTokens()
	{
		_takenTiles = 0;
		Tokens = _gameplayManager.CurrentPhase.Tokens;
		OnRefreshTokensEvent?.Invoke();
	}

	private void OnTileClicked(MapTileData tileData)
	{
		if (Tokens == 0)
		{
			return;
		}

		if (tileData.OwnerPlayerIndex == -1 && CanTakeTile())
		{
			_takenTiles++;

			PutTokenOnBoard(tileData);
		}
		else if (tileData.OwnerPlayerIndex == _gameplayManager.CurrentPlayerIndex)
		{
			PutTokenOnBoard(tileData);
		}
	}

	private bool CanTakeTile()
	{
		return _takenTiles < _gameplayManager.CurrentPhase.TilesLimit;
	}

	public void PutTokenOnBoard(MapTileData tileData)
	{
		tileData.OwnerPlayerIndex = _gameplayManager.CurrentPlayerIndex;
		tileData.Tokens++;
		Tokens--;
		OnRefreshTokensEvent?.Invoke();
	}

	private void OnPlayerChanged()
	{
		RefreshTokens();
	}

	private void Deinitialize()
	{
		UnbindFromEvents();
	}

	private void UnbindFromEvents()
	{
		_gameplayManager.OnPhaseChangedEvent -= OnPhaseChanged;
		_mapController.OnTileClickedEvent -= OnTileClicked;
		_gameplayManager.OnPlayerChangedEvent -= OnPlayerChanged;
	}
}
