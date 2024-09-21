using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour, ISceneController
{
	public bool IsLoading { get; private set; }

	public event Action OnStartSceneLoadingEvent;
	public event Action<SceneSetup> OnSceneLoadedEvent;

	public async void LoadSceneSetup(SceneSetup sceneSetup)
	{
		IsLoading = true;
		OnStartSceneLoadingEvent?.Invoke();

		await LoadNewScene(sceneSetup);

		OnSceneLoadedEvent?.Invoke(sceneSetup);
		IsLoading = false;
	}

	private async UniTask LoadNewScene(SceneSetup sceneSetup)
	{
		await SceneManager.LoadSceneAsync(sceneSetup.SceneName).ToUniTask();
	}
}
