using UnityEngine;
using UnityEngine.UI;

public class ChangeScale : MonoBehaviour
{
	[SerializeField]
	private Vector3 _selectedScale = new Vector3(1.1f, 1.1f, 1.1f);

	public void Select()
	{
		GetComponent<RectTransform>().localScale = _selectedScale;
		GetComponent<Image>().color = Color.gray;
	}

	public void Deselect()
	{
		GetComponent<RectTransform>().localScale = Vector3.one;
		GetComponent<Image>().color = Color.white;

	}
}
