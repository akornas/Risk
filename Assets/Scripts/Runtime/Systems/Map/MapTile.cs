using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapTile : MonoBehaviour
{
	public event System.Action<MapTile> OnSelectedEvent;
	public event System.Action<MapTile> OnDeselectedEvent;
	public event System.Action<MapTile> OnClickEvent;

	[Inject]
	private readonly ISettingsController _settingsController;

	[SerializeField]
	private Image _image;

	[SerializeField]
	private MapTileData _data = new();

	[SerializeField]
	private List<MapTile> _neighbourTiles;

	public MapTileData Data => _data;

	#region EditorHelpers
	private void OnValidate()
	{
		foreach (var neighbourTile in _neighbourTiles)
		{
			if (neighbourTile != null)
			{
				neighbourTile.AddNeighbourTile(this);
			}
		}
	}

	public void AddNeighbourTile(MapTile tile)
	{
		if (!_neighbourTiles.Contains(tile))
		{
			_neighbourTiles.Add(tile);
		}
	}

	private void Reset()
	{
		if (_image == null)
		{
			_image = GetComponentInChildren<Image>();
		}
	}

	[Button]
	public void SwitchAllNeighbours()
	{
		foreach (var neighbourTile in _neighbourTiles)
		{
			if (neighbourTile != null)
			{
				neighbourTile.gameObject.SetActive(!neighbourTile.gameObject.activeInHierarchy);
			}
		}
		_image.color = _neighbourTiles[0].isActiveAndEnabled ? Color.white : Color.red;
	}
	#endregion

	[Inject]
	public void Initialize()
	{
		Data.Tokens = Random.Range(1, 20);
		Data.OnDataChangedEvent += OnDataChanged;
	}

	private void OnDataChanged()
	{
		_image.color = _settingsController.GetColorForPlayer(Data.OwnerPlayerIndex);
	}

	public void Select()
	{
		OnSelectedEvent?.Invoke(this);
	}

	public void Deselect()
	{
		OnDeselectedEvent?.Invoke(this);
	}

	public void Click()
	{
		OnClickEvent?.Invoke(this);
	}

	private void OnDestroy()
	{
		Data.OnDataChangedEvent -= OnDataChanged;
	}

	public bool IsTileInNeighbour(MapTile otherTile)
	{
		return _neighbourTiles.Contains(otherTile);
	}
}
