using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapTileDescriptionUi : MonoBehaviour
{
	[Inject]
	private readonly IMapController _mapController;

	[Inject]
	private readonly ISettingsController _settingsController;

	[Inject]
	private readonly ITokensController _tokensController;

	[SerializeField]
	private GameObject _root;

	[SerializeField]
	private TextMeshProUGUI _playerLabel;

	[SerializeField]
	private TextMeshProUGUI _tokensLabel;

	[SerializeField]
	private Image _flagImage;

	private MapTile _lastSelectedTile;

	private int _selectedCount = 0;

	[Inject]
	public void Initialize()
	{
		_mapController.OnTileSelectedEvent += OnTileSelected;
		_mapController.OnTileDeselectedEvent += OnTileDeselected;
		_tokensController.OnRefreshTokensEvent += OnRefreshTokens;
	}

	private void OnRefreshTokens()
	{
		if (_lastSelectedTile != null)
		{
			OnTileSelected(_lastSelectedTile);
		}
	}

	private void OnTileSelected(MapTile tile)
	{
		_selectedCount++;
		_lastSelectedTile = tile;

		_root.SetActive(true);
		_playerLabel.gameObject.SetActive(tile.Data.OwnerPlayerIndex != -1);
		_playerLabel.text = $"{SettingsController.PLAYER_LABEL} {tile.Data.OwnerPlayerIndex + 1}";
		_tokensLabel.text = $"{tile.Data.Tokens}";
		_flagImage.color = _settingsController.GetColorForPlayer(tile.Data.OwnerPlayerIndex);
	}

	private void OnTileDeselected(MapTile tile)
	{
		_selectedCount--;

		if (_root.activeInHierarchy)
		{
			_root.SetActive(_selectedCount == 0);
		}
	}

	private void OnDestroy()
	{
		_mapController.OnTileSelectedEvent -= OnTileSelected;
		_mapController.OnTileDeselectedEvent -= OnTileDeselected;
		_tokensController.OnRefreshTokensEvent -= OnRefreshTokens;
	}
}
