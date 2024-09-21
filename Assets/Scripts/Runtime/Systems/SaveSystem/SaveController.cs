using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SaveController : MonoBehaviour, ISaveController
{
	private ISaveSystem _saveSystem;

	public GameplayData GameplayDataFromSave { get; private set; }

	[Inject]
	public void Initialize()
	{
		_saveSystem =
#if UNITY_STANDALONE && !OTHER_PLATFORM
			new StandaloneSaveSystem();
#elif OTHER_PLATFORM
			new StandaloneSaveSystem();
#endif
	}

	public async UniTask SaveData(GameplayData data)
	{
		await _saveSystem.SaveData(data);
	}

	public async UniTask LoadData()
	{
		GameplayDataFromSave = await _saveSystem.LoadData();
	}

	public bool SaveExists()
	{
		return _saveSystem.SaveExists();
	}

	public void DeleteSave()
	{
		_saveSystem.DeleteSave();
	}
}
