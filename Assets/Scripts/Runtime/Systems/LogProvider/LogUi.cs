using TMPro;
using UnityEngine;

public class LogUi : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _logLabel;

	public void Initialize(string message)
	{
		_logLabel.text = message;
	}
}
