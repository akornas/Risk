using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
	[Inject]
	private readonly IGameplaySettingsManager _gameplaySettingsManager;

	[Inject]
	private readonly ISaveController _saveController;

	[SerializeField]
	private SceneSetupLoader _sceneSetupLoader;

	public async void LoadGame()
	{
		await LoadSettingsDataFromSave();
		_sceneSetupLoader.Load();
	}

	public async UniTask LoadSettingsDataFromSave()
	{
		await _saveController.LoadData();
		_gameplaySettingsManager.CreateSettingsData(_saveController.GameplayDataFromSave.GameplaySettingsData);
	}
}
