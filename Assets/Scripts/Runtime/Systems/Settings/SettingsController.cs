using UnityEngine;

public class SettingsController : MonoBehaviour, ISettingsController
{
	public const string PLAYER_LABEL = "<size=36>PLAYER</size>";
	public const string TOKENS_LABEL = "<size=36>Tokens</size>";

	[SerializeField]
	private SettingsData _settingsData;

	public SettingsData SettingsData => _settingsData;

	public Color GetColorForPlayer(int index)
	{
		return index switch
		{
			0 => SettingsData.Player1Color,
			1 => SettingsData.Player2Color,
			2 => SettingsData.Player3Color,
			3 => SettingsData.Player4Color,
			_ => Color.white,
		};
	}
}
