using Zenject;

public class SettingUpPhase : AbstractPhase
{
	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly ITokensController _tokensController;

	[Inject]
	private readonly ILogProvider _logProvider;

	public override int TilesLimit => 2;
	public override int Tokens => _gameplayManager.GameplayData.GameplaySettingsData.Tokens;
	public override bool CanEndTurn => _tokensController.Tokens == 0;
	public override bool CanBeEnded => true;

	public override GamePhaseType PhaseType => GamePhaseType.SettingUp;

	[Inject]
	public void Initialize()
	{
		_tokensController.IsEnabled = true;
	}

	public override void NotifyWhyCanNotEndTurn()
	{
		_logProvider.Log("You have to lay out all the tokens");
	}

	public override void CleanUp()
	{
		_tokensController.IsEnabled = false;
	}
}

