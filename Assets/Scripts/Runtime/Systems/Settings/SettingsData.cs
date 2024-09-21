using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "Risk/Settings/Data")]
public class SettingsData : ScriptableObject
{
	[SerializeField]
	private Color _player1Color;

	[SerializeField]
	private Color _player2Color;

	[SerializeField]
	private Color _player3Color;

	[SerializeField]
	private Color _player4Color;

	public Color Player1Color => _player1Color;
	public Color Player2Color => _player2Color;
	public Color Player3Color => _player3Color;
	public Color Player4Color => _player4Color;
}
