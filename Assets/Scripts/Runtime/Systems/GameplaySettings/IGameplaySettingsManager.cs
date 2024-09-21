public interface IGameplaySettingsManager
{
	GameplaySettingsData GameplaySettingsData { get; }
	void CreateSettingsData(int players, int rounds, int tokens, int dices);
	void CreateSettingsData(GameplaySettingsData _data);
}
