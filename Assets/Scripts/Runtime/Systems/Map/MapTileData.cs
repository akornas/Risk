using UnityEngine;

[System.Serializable]
public class MapTileData
{
	public event System.Action OnDataChangedEvent;

	[SerializeField]
	private int _ownerPlayerIndex = -1;

	[SerializeField]
	private int _tokens = 0;

	public int OwnerPlayerIndex
	{
		get => _ownerPlayerIndex;
		set
		{
			if (_ownerPlayerIndex != value)
			{
				_ownerPlayerIndex = value;
				OnDataChangedEvent?.Invoke();
			}
		}
	}

	public int Tokens
	{
		get => _tokens;
		set
		{
			if (_tokens != value)
			{
				_tokens = value;
				OnDataChangedEvent?.Invoke();
			}
		}
	}
}
