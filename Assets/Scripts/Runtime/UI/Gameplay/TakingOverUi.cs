using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TakingOverUi : MonoBehaviour
{
	[Inject]
	private readonly ITakingOverController _takingOverController;

	[Inject]
	private readonly ISettingsController _settingsController;

	[SerializeField]
	private TakingOverUiSlot _attackerSlot;

	[SerializeField]
	private TakingOverUiSlot _defenderSlot;

	[SerializeField]
	private GameObject _root;

	[SerializeField]
	private GameObject _attackButton;

	[SerializeField]
	private TextMeshProUGUI _percentageLabel;

	[Inject]
	public void Initialize()
	{
		_takingOverController.OnSelectedAttackerTileEvent += OnSelectedAttackerTile;
		_takingOverController.OnSelectedDefenderTileEvent += OnSelectedDefenderTile;
		_takingOverController.OnInitializedEvent += OnControllerInitialized;
		_takingOverController.OnRefreshEvent += OnRefreshControlerData;
		_takingOverController.OnAfterAttackEvent += OnAfterAttack;
	}

	private void OnControllerInitialized()
	{
		_attackButton.SetActive(false);
		_root.SetActive(true);
	}

	private void OnSelectedAttackerTile(MapTileData tileData)
	{
		_attackerSlot.Container.SetActive(true);
		_attackerSlot.TokensLabel.text = $"{tileData.Tokens}";
		_attackerSlot.Flag.color = _settingsController.GetColorForPlayer(tileData.OwnerPlayerIndex);
	}

	private void OnSelectedDefenderTile(MapTileData tileData)
	{
		_defenderSlot.Container.SetActive(true);
		_defenderSlot.TokensLabel.text = $"{tileData.Tokens}";
		_defenderSlot.Flag.color = _settingsController.GetColorForPlayer(tileData.OwnerPlayerIndex);

		_attackButton.SetActive(true);
	}

	private void OnRefreshControlerData()
	{
		_percentageLabel.text = $"{_takingOverController.PercentageChance:f0}%";
	}

	private void OnAfterAttack()
	{
		_attackButton.SetActive(false);
		_attackerSlot.Container.SetActive(false);
		_defenderSlot.Container.SetActive(false);
	}

	private void OnDestroy()
	{
		_takingOverController.OnSelectedAttackerTileEvent -= OnSelectedAttackerTile;
		_takingOverController.OnSelectedDefenderTileEvent -= OnSelectedDefenderTile;
		_takingOverController.OnInitializedEvent -= OnControllerInitialized;
		_takingOverController.OnAfterAttackEvent -= OnAfterAttack;
	}
}

[System.Serializable]
public class TakingOverUiSlot
{
	public GameObject Container;
	public TextMeshProUGUI TokensLabel;
	public Image Flag;
}
