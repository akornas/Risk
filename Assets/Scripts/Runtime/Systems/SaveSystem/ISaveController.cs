using Cysharp.Threading.Tasks;

public interface ISaveController
{
	GameplayData GameplayDataFromSave { get; }
	UniTask SaveData(GameplayData data);
	UniTask LoadData();
	bool SaveExists();
	void DeleteSave();
}
