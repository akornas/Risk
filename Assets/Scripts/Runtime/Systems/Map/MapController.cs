using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapController : MonoBehaviour, IMapController
{
	public event Action<MapTileData> OnTileSelectedEvent;
	public event Action<MapTileData> OnTileDeselectedEvent;
	public event Action<MapTileData> OnTileClickedEvent;

	private readonly List<MapTile> _tiles = new();

	public List<MapTile> Tiles => _tiles;

	[Inject]
	public void Initialize()
	{
		CollectTiles();
		BindToCollectedTiles();
	}

	private void CollectTiles()
	{
		foreach (Transform child in transform)
		{
			var tile = child.GetComponent<MapTile>();
			_tiles.Add(tile);
		}
	}

	private void BindToCollectedTiles()
	{
		foreach (var tile in _tiles)
		{
			tile.OnSelectedEvent += OnTileSelected;
			tile.OnDeselectedEvent += OnTileDeselected;
			tile.OnClickEvent += OnTileClicked;
		}
	}

	private void OnTileSelected(MapTileData tileData)
	{
		OnTileSelectedEvent?.Invoke(tileData);
	}

	private void OnTileDeselected(MapTileData tileData)
	{
		OnTileDeselectedEvent?.Invoke(tileData);
	}

	private void OnTileClicked(MapTileData tileData)
	{
		OnTileClickedEvent?.Invoke(tileData);
	}

	private void OnDestroy()
	{
		UnbindFromTiles();
	}

	private void UnbindFromTiles()
	{
		foreach (var tile in _tiles)
		{
			tile.OnSelectedEvent -= OnTileSelected;
			tile.OnDeselectedEvent -= OnTileDeselected;
			tile.OnClickEvent -= OnTileClicked;
		}
	}
}
