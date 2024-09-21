using UnityEngine;
using Zenject;

public class LifetimeSystemsInstaller : MonoInstaller
{
	[SerializeField]
	private SceneController _sceneController;

	[SerializeField]
	private GameplaySettingsManager _gameplaySettingsManager;

	[SerializeField]
	private SaveController _saveController;

	[SerializeField]
	private SettingsController _settingsController;

	public override void InstallBindings()
	{
		Container.Bind<ISceneController>().FromComponentInNewPrefab(_sceneController).AsSingle();
		Container.Bind<IGameplaySettingsManager>().FromComponentInNewPrefab(_gameplaySettingsManager).AsSingle();
		Container.Bind<ISaveController>().FromComponentInNewPrefab(_saveController).AsSingle();
		Container.Bind<ISettingsController>().FromComponentInNewPrefab(_settingsController).AsSingle();

		Container.Bind<IEventSystemProvider>().To<EventSystemProvider>().AsSingle();
	}
}
