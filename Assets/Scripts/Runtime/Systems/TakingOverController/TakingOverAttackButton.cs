using UnityEngine;
using Zenject;

public class TakingOverAttackButton : MonoBehaviour
{
	[Inject]
	private readonly ITakingOverController _takingOverController;

	public void Attack()
	{
		_takingOverController.Attack();
	}
}
