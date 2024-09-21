using UnityEngine;

public interface ISettingsController
{
	SettingsData SettingsData { get; }
	Color GetColorForPlayer(int index);
}
