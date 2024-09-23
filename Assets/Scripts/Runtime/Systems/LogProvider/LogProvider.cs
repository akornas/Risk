using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

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

	[Inject]
	private readonly IGameplayManager _gameplayManager;

	[Inject]
	private readonly ISettingsController _settingsController;

	private readonly CancellationTokenSource _cancellationToken = new();
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

	public void Log(string message)
	{
		var logUiFromPool = GetLogUiFromPool();
		message = $"{GetPrefixColorForCurrentMessage()}{message}</color>";
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

	private string GetPrefixColorForCurrentMessage()
	{
		return $"<color=#{_settingsController.GetColorForPlayer(_gameplayManager.CurrentPlayerIndex).ToHexString()}>";
	}

	private async UniTask ReturnLogUiToPoolAfterLifetime()
	{
		await UniTask.Delay(MessageLifeTimeInMiliseconds, cancellationToken: _cancellationToken.Token).SuppressCancellationThrow();

		if (!_cancellationToken.IsCancellationRequested)
		{
			var message = _createdMessages.Dequeue();
			message.transform.SetParent(_messagePoolRoot);
			_messagePool.Push(message);
		}
	}

	private void OnDestroy()
	{
		_cancellationToken.Cancel();
	}
}
