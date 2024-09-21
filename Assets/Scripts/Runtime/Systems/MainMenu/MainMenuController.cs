using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuController : MonoBehaviour
{
	[Inject]
	private readonly ISaveController _saveController;

	[SerializeField]
	private AbstractUiPanel _mainMenuPanel;

	[SerializeField]
	private AbstractUiPanel _gameplaySettingsPanel;

	[SerializeField]
	private Button _continueButton;

	private AbstractUiPanel _activePanel;

	public void OnEnable()
	{
		HandleContinueButton();
		ShowMainMenu();
	}

	private void HandleContinueButton()
	{
		_continueButton.interactable = _saveController.SaveExists();
	}

	public async void ShowMainMenu()
	{
		await CloseCurrentPanel();
		SetActivePanel(_mainMenuPanel);
		await OpenCurrentPanel();
	}

	public async void ShowGameplaySettings()
	{
		await CloseCurrentPanel();
		SetActivePanel(_gameplaySettingsPanel);
		await OpenCurrentPanel();
	}

	private void SetActivePanel(AbstractUiPanel newActivePanel)
	{
		_activePanel = newActivePanel;
	}

	private async UniTask OpenCurrentPanel()
	{
		if (_activePanel != null)
		{
			await _activePanel.Open();
		}
	}

	private async UniTask CloseCurrentPanel()
	{
		if (_activePanel != null)
		{
			await _activePanel.Close();
		}
	}
}
