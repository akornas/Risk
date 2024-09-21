using UnityEngine;
using Zenject;

public class SceneSetupLoader : MonoBehaviour
{
	[Inject]
	private readonly ISceneController _sceneController;

	[SerializeField]
	private SceneSetup _sceneToLoad;

	public void Load()
	{
		if (_sceneController.IsLoading)
		{
			return;
		}

		_sceneController.LoadSceneSetup(_sceneToLoad);
	}
}
