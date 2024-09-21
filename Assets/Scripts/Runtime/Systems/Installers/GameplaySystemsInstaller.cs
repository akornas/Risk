using UnityEngine;
using Zenject;

public class GameplaySystemsInstaller : MonoInstaller
{
	[SerializeField]
	private GameplayManager _gameplayController;

	[SerializeField]
	private MapController _mapController;

	public override void InstallBindings()
	{
		Container.Bind<IGameplayManager>().FromComponentInNewPrefab(_gameplayController).AsSingle();
		Container.Bind<IMapController>().FromComponentInHierarchy(_mapController).AsSingle();

		Container.Bind<ITokensController>().To<TokensController>().AsSingle();
		Container.Bind<ITakingOverController>().To<TakingOverController>().AsSingle();
		Container.Bind<IWinLoseProvider>().To<WinLoseProvider>().AsSingle();

		Container.BindFactory<SettingUpPhase, Factory<SettingUpPhase>>();
		Container.BindFactory<TakingOverPhase, Factory<TakingOverPhase>>();
	}
}
