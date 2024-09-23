using Zenject;

public class TakingOverPhase : AbstractPhase, IInitializable
{
	[Inject]
	private readonly ITakingOverController _takingOverController;

	[Inject]
	private readonly ILogProvider _logProvider;

	public override int TilesLimit => 1;
	public override bool CanEndTurn => _takingOverController.IsAfterAttack;
	public override bool CanBeEnded => false;

	public override int Tokens => 0;

	public override GamePhaseType PhaseType => GamePhaseType.TakingOver;

	[Inject]
	public void Initialize()
	{
		_takingOverController.IsEnabled = true;
	}

	public override void NotifyWhyCanNotEndTurn()
	{
		_logProvider.Log("You have to attack first");
	}

	public override void CleanUp()
	{
		_takingOverController.IsEnabled = false;
	}
}
