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

	private MapTileData _lastSelectedData;

	[Inject]
	public void Initialize()
	{
		_mapController.OnTileSelectedEvent += OnTileSelected;
		_mapController.OnTileDeselectedEvent += OnTileDeselected;
		_tokensController.OnRefreshTokensEvent += OnRefreshTokens;
	}

	private void OnRefreshTokens()
	{
		if (_lastSelectedData != null)
		{
			OnTileSelected(_lastSelectedData);
		}
	}

	private void OnTileSelected(MapTileData data)
	{
		_lastSelectedData = data;

		if (data.OwnerPlayerIndex != -1)
		{
			_root.SetActive(true);
			_playerLabel.text = $"{SettingsController.PLAYER_LABEL} {data.OwnerPlayerIndex + 1}";
			_tokensLabel.text = $"{data.Tokens}";
			_flagImage.color = _settingsController.GetColorForPlayer(data.OwnerPlayerIndex);
		}
	}

	private void OnTileDeselected(MapTileData data)
	{
		if (_root.activeInHierarchy)
		{
			_root.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		_mapController.OnTileSelectedEvent -= OnTileSelected;
		_mapController.OnTileDeselectedEvent -= OnTileDeselected;
		_tokensController.OnRefreshTokensEvent -= OnRefreshTokens;
	}
}
