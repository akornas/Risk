using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemProvider : IEventSystemProvider
{
	public void SetSelectedGameObject(GameObject gameObject)
	{
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
