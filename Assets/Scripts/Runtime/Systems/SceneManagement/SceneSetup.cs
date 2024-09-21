using UnityEngine;

[CreateAssetMenu(fileName = "SceneSetup", menuName = "Risk/SceneManagement/SceneSetup")]
public class SceneSetup : ScriptableObject
{
	[SerializeField]
	private string _sceneName;

	public string SceneName => _sceneName;
}
