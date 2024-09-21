using TMPro;
using UnityEngine;
using Zenject;

public class RoundUi : MonoBehaviour
{
	[Inject]
	private readonly IGameplayManager _gameplayController;

	[SerializeField]
	private TextMeshProUGUI _roundLabel;

	[Inject]
	public void Initialize()
	{
		_gameplayController.OnRoundChangedEvent += OnRoundChanged;
		OnRoundChanged();
	}

	private void OnRoundChanged()
	{
		_roundLabel.text = $"{_gameplayController.CurrentRound}";
	}

	private void OnDestroy()
	{
		_gameplayController.OnRoundChangedEvent -= OnRoundChanged;
	}
}
