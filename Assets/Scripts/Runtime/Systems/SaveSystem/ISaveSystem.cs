using Cysharp.Threading.Tasks;

public interface ISaveSystem
{
	string SavePath { get; }
	UniTask SaveData(GameplayData data);
	UniTask<GameplayData> LoadData();
	bool SaveExists();
	void DeleteSave();
}
