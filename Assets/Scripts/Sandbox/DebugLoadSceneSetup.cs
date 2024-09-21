using UnityEngine;

public class DebugLoadSceneSetup : MonoBehaviour
{
	[SerializeField]
	private SceneSetup _sceneSetup;

	[SerializeField]
	private SceneController _sceneController;

	public void LoadSceneSetup()
	{
		_sceneController.LoadSceneSetup(_sceneSetup);
	}
}
