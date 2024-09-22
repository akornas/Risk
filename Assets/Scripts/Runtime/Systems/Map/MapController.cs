using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MapController : MonoBehaviour, IMapController
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	public event Action<MapTile> OnTileSelectedEvent;
	public event Action<MapTile> OnTileDeselectedEvent;
	public event Action<MapTile> OnTileClickedEvent;

	private readonly List<MapTile> _tiles = new();

	public List<MapTile> Tiles => _tiles;

	[Inject]
	public void Initialize()
	{
		CollectTiles();
		BindToCollectedTiles();
	}

	private void OnGameplayDataInitialized()
	{
		var tileDatas = _gameplayManager.GameplayData.TileDatas;

		foreach (var tile in _tiles)
		{
			var savedData = tileDatas.FirstOrDefault(savedTile => savedTile.Guid == tile.Data.Guid);
			if (savedData != null)
			{
				tile.Data.SetData(savedData);
			}
		}
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

	private void OnTileSelected(MapTile tile)
	{
		OnTileSelectedEvent?.Invoke(tile);
	}

	private void OnTileDeselected(MapTile tile)
	{
		OnTileDeselectedEvent?.Invoke(tile);
	}

	private void OnTileClicked(MapTile tile)
	{
		OnTileClickedEvent?.Invoke(tile);
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
