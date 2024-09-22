using UnityEngine;
using Zenject;

public class GameplaySystemsInstaller : MonoInstaller
{
	[SerializeField]
	private GameplayManager _gameplayManager;

	public override void InstallBindings()
	{
		Container.Bind<IGameplayManager>().FromComponentInNewPrefab(_gameplayManager).AsSingle();

		Container.Bind<ILogProvider>().FromComponentInHierarchy().AsSingle();
		Container.Bind<IMapController>().FromComponentInHierarchy().AsSingle();

		Container.Bind<ITokensController>().To<TokensController>().AsSingle();
		Container.Bind<ITakingOverController>().To<TakingOverController>().AsSingle();
		Container.Bind<IWinLoseProvider>().To<WinLoseProvider>().AsSingle();
		Container.Bind<IBoardState>().To<BoardState>().AsSingle();

		Container.BindFactory<SettingUpPhase, Factory<SettingUpPhase>>();
		Container.BindFactory<TakingOverPhase, Factory<TakingOverPhase>>();
	}
}
