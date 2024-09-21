using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplaySettingsUi : MonoBehaviour
{
	[SerializeField]
	private GameplaySettingsDefaultData _gameplaySettingsDefaultData;

	[SerializeField]
	private SliderLabel _playersSliderLabel;

	[SerializeField]
	private SliderLabel _roundsSliderLabel;

	[SerializeField]
	private SliderLabel _tokensSliderLabel;

	[SerializeField]
	private SliderLabel _dicesSliderLabel;

	[Inject]
	private readonly IGameplaySettingsManager _gameplaySettingsManager;

	private GameplaySettingsData MinimalRange => _gameplaySettingsDefaultData.MinimalRange;
	private GameplaySettingsData MaximalRange => _gameplaySettingsDefaultData.MaximalRange;
	private GameplaySettingsData DefaultValue => _gameplaySettingsDefaultData.DefaultValue;

	private void OnEnable()
	{
		InitializeSliders();
	}

	private void InitializeSliders()
	{
		_playersSliderLabel.Initialize(MinimalRange.Players, MaximalRange.Players, DefaultValue.Players);
		_roundsSliderLabel.Initialize(MinimalRange.Rounds, MaximalRange.Rounds, DefaultValue.Rounds);
		_tokensSliderLabel.Initialize(MinimalRange.Tokens, MaximalRange.Tokens, DefaultValue.Tokens);
		_dicesSliderLabel.Initialize(MinimalRange.Dices, MaximalRange.Dices, DefaultValue.Dices);
	}

	public void CreateSettingsData()
	{
		var playersCount = _playersSliderLabel.Value;
		var roundsCount = _roundsSliderLabel.Value;
		var tokensCount = _tokensSliderLabel.Value;
		var dicesCount = _dicesSliderLabel.Value;

		_gameplaySettingsManager.CreateSettingsData(playersCount, roundsCount, tokensCount, dicesCount);
	}

	private void OnDisable()
	{
		_playersSliderLabel.CleanUp();
		_roundsSliderLabel.CleanUp();
		_tokensSliderLabel.CleanUp();
		_dicesSliderLabel.CleanUp();
	}
}

[System.Serializable]
public class SliderLabel
{
	public Slider Slider;
	public TextMeshProUGUI Label;

	public int Value => Mathf.RoundToInt(Slider.value);

	public void Initialize(int minimalRange, int maximalRange, int defaultValue)
	{
		AddListener();

		Slider.minValue = minimalRange;
		Slider.maxValue = maximalRange;
		Slider.value = defaultValue;
	}

	private void AddListener()
	{
		Slider.onValueChanged.AddListener(delegate { UpdateLabel(); });
	}

	private void UpdateLabel()
	{
		Label.text = $"{Slider.value}";
	}

	public void CleanUp()
	{
		Slider.onValueChanged.RemoveAllListeners();
	}
}
