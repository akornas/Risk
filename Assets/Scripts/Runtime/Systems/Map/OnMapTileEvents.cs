using UnityEngine;
using UnityEngine.Events;

public class OnMapTileEvents : MonoBehaviour
{
	public UnityEvent OnSelectedUnityEvent;
	public UnityEvent OnDeselectedUnityEvent;
	public UnityEvent OnClickedUnityEvent;

	private MapTile _mapTile;

	private void Awake()
	{
		_mapTile = GetComponent<MapTile>();

		_mapTile.OnClickEvent += OnClick;
		_mapTile.OnDeselectedEvent += OnDeselected;
		_mapTile.OnSelectedEvent += OnSelected;
	}

	private void OnSelected(MapTile obj)
	{
		OnSelectedUnityEvent?.Invoke();
	}

	private void OnDeselected(MapTile obj)
	{
		OnDeselectedUnityEvent?.Invoke();
	}

	private void OnClick(MapTile obj)
	{
		OnClickedUnityEvent?.Invoke();
	}

	private void OnDestroy()
	{
		_mapTile.OnClickEvent -= OnClick;
		_mapTile.OnDeselectedEvent -= OnDeselected;
		_mapTile.OnSelectedEvent -= OnSelected;
	}
}
