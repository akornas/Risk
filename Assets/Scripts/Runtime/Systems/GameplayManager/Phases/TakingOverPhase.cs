using UnityEngine;
using Zenject;

public class TakingOverPhase : AbstractPhase
{
	[Inject]
	private readonly ITakingOverController _takingOverController;

	public override int TilesLimit => 1;
	public override bool CanEndTurn => _takingOverController.IsAfterAttack;
	public override bool CanBeEnded => false;

	public override int Tokens => 0;

	[Inject]
	public void Initialize()
	{
		_takingOverController.IsEnabled = true;
	}

	public override void NotifyWhyCanNotEndTurn()
	{
		Debug.Log("You have to attack at least once");
	}

	public override void CleanUp()
	{
		_takingOverController.IsEnabled = false;
	}
}
