using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;

public class StandaloneSaveSystem : ISaveSystem
{
	private const string SAVE_FILE_NAME = "Save.sav";
	public string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

	public async UniTask SaveData(GameplayData data)
	{
		var jsonData = JsonUtility.ToJson(data);

		await File.WriteAllTextAsync(SavePath, jsonData);
	}

	public async UniTask<GameplayData> LoadData()
	{
		var readData = await File.ReadAllTextAsync(SavePath);

		return JsonUtility.FromJson<GameplayData>(readData);
	}

	public void DeleteSave()
	{
		if (SaveExists())
		{
			File.Delete(SavePath);
		}
	}

	public bool SaveExists()
	{
		return File.Exists(SavePath);
	}
}
