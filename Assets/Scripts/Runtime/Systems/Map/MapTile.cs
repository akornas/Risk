using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapTile : MonoBehaviour
{
	public event System.Action<MapTileData> OnSelectedEvent;
	public event System.Action<MapTileData> OnDeselectedEvent;
	public event System.Action<MapTileData> OnClickEvent;

	[Inject]
	private readonly ISettingsController _settingsController;

	[SerializeField]
	private Image _image;

	private readonly MapTileData _data = new();

	public MapTileData Data => _data;

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
		OnSelectedEvent?.Invoke(Data);
	}

	public void Deselect()
	{
		OnDeselectedEvent?.Invoke(Data);
	}

	public void Click()
	{
		OnClickEvent?.Invoke(Data);
	}

	private void OnDestroy()
	{
		Data.OnDataChangedEvent -= OnDataChanged;
	}
}
