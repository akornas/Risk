using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapController : MonoBehaviour, IMapController
{
	[Inject]
	private readonly IEndGame _endGameManager;

	public event Action<MapTile> OnTileSelectedEvent;
	public event Action<MapTile> OnTileDeselectedEvent;
	public event Action<MapTile> OnTileClickedEvent;
	public event Action OnSetActiveEvent;

	private readonly List<MapTile> _tiles = new();
	private bool _isActive;

	public List<MapTile> Tiles => _tiles;

	public bool IsActive
	{
		get;
		private set;
	}

	[Inject]
	public void Initialize()
	{
		CollectTiles();
		BindToCollectedTiles();
		_endGameManager.OnEndGameEvent += OnEndGame;
		IsActive = true;
		OnSetActiveEvent?.Invoke();
	}

	private void OnEndGame()
	{
		IsActive = false;
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

	private void UnbindFromTiles()
	{
		foreach (var tile in _tiles)
		{
			tile.OnSelectedEvent -= OnTileSelected;
			tile.OnDeselectedEvent -= OnTileDeselected;
			tile.OnClickEvent -= OnTileClicked;
		}
	}

	public void OnDestroy()
	{
		_endGameManager.OnEndGameEvent -= OnEndGame;
		UnbindFromTiles();
	}
}
