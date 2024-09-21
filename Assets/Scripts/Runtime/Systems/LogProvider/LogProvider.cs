using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class LogProvider : MonoBehaviour, ILogProvider
{
	[SerializeField]
	private LogUi _logUiprefab;

	[SerializeField]
	private float _messageLifeTime = 5f;

	[SerializeField]
	private Transform _root;

	[SerializeField]
	private Transform _messagePoolRoot;

	[SerializeField]
	private float _poolSize = 5;

	private readonly Stack<LogUi> _messagePool = new();
	private readonly Queue<LogUi> _createdMessages = new();
	private int MessageLifeTimeInMiliseconds => Mathf.RoundToInt(_messageLifeTime * 1000);

	private void Awake()
	{
		FillPool();
	}

	private void FillPool()
	{
		for (int i = 0; i < _poolSize; i++)
		{
			CreateLogUi();
		}
	}

	private void CreateLogUi()
	{
		var createdLogUi = Instantiate(_logUiprefab, _messagePoolRoot);
		_messagePool.Push(createdLogUi);
	}

	public void ShowMessage(string message)
	{
		var logUiFromPool = GetLogUiFromPool();

		logUiFromPool.Initialize(message);
		logUiFromPool.transform.SetParent(_root);
		_createdMessages.Enqueue(logUiFromPool);
		_ = ReturnLogUiToPoolAfterLifetime();
	}

	private LogUi GetLogUiFromPool()
	{
		if (_messagePool.Count == 0)
		{
			CreateLogUi();
		}

		return _messagePool.Pop();
	}

	private async UniTask ReturnLogUiToPoolAfterLifetime()
	{
		await UniTask.Delay(MessageLifeTimeInMiliseconds);
		var message = _createdMessages.Dequeue();
		message.transform.SetParent(_messagePoolRoot);
		_messagePool.Push(message);
	}
}
