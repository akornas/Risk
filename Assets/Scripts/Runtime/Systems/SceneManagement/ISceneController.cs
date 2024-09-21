using System;

public interface ISceneController
{
	event Action OnStartSceneLoadingEvent;
	event Action<SceneSetup> OnSceneLoadedEvent;
	void LoadSceneSetup(SceneSetup sceneSetup);
	bool IsLoading { get; }
}