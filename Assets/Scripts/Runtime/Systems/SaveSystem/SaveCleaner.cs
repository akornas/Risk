using UnityEngine;
using Zenject;

public class SaveCleaner : MonoBehaviour
{
	[Inject]
	private readonly ISaveController _saveController;

	public void ClearDataSave()
	{
		_saveController.DeleteSave();
	}
}
