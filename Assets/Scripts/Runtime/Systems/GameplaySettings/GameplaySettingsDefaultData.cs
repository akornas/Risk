using UnityEngine;

[CreateAssetMenu(fileName = "GameplaySettingsDefaultData", menuName = "Risk/GameplaySettings/DefaultData")]
public class GameplaySettingsDefaultData : ScriptableObject
{
	[SerializeField]
	private GameplaySettingsData _minimalRange;

	[SerializeField]
	private GameplaySettingsData _maximalRange;

	[SerializeField]
	private GameplaySettingsData _defaultValue;

	public GameplaySettingsData MinimalRange => _minimalRange;
	public GameplaySettingsData MaximalRange => _maximalRange;
	public GameplaySettingsData DefaultValue => _defaultValue;
}
