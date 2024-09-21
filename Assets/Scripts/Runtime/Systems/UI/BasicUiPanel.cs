using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BasicUiPanel : AbstractUiPanel
{
	[SerializeField]
	private GameObject _firstSelected;

	[Inject]
	private readonly IEventSystemProvider _eventSystemProvider;

	protected override UniTask OnClose()
	{
		return UniTask.CompletedTask;
	}

	protected override UniTask OnOpen()
	{
		_eventSystemProvider.SetSelectedGameObject(_firstSelected);
		return UniTask.CompletedTask;
	}
}
