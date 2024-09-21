using UnityEngine;
using Zenject;

public class GameplaySystemsInstaller : MonoInstaller
{
	[SerializeField]
	private GameplayManager _gameplayController;

	[SerializeField]
	private MapController _mapController;

	[SerializeField]
	private LogProvider _logProvider;

	public override void InstallBindings()
	{
		Container.Bind<IGameplayManager>().FromComponentInNewPrefab(_gameplayController).AsSingle();

		Container.Bind<IMapController>().FromComponentInHierarchy(_mapController).AsSingle();
		Container.Bind<ILogProvider>().FromComponentInHierarchy(_logProvider).AsSingle();

		Container.Bind<ITokensController>().To<TokensController>().AsSingle();
		Container.Bind<ITakingOverController>().To<TakingOverController>().AsSingle();
		Container.Bind<IWinLoseProvider>().To<WinLoseProvider>().AsSingle();
		Container.Bind<IBoardState>().To<BoardState>().AsSingle();

		Container.BindFactory<SettingUpPhase, Factory<SettingUpPhase>>();
		Container.BindFactory<TakingOverPhase, Factory<TakingOverPhase>>();
	}
}
