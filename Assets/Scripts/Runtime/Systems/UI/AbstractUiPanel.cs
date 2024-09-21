using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class AbstractUiPanel : MonoBehaviour
{
	public bool IsAnimating { get; private set; }

	public async UniTask Open()
	{
		this.gameObject.SetActive(true);
		IsAnimating = true;
		await OnOpen();
		IsAnimating = false;
	}

	protected abstract UniTask OnOpen();

	public async UniTask Close()
	{
		IsAnimating = true;
		await OnClose();
		IsAnimating = false;
		this.gameObject.SetActive(false);
	}

	protected abstract UniTask OnClose();
}
