using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using BehaviorTree;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Threading;
using System;

public class PlayerAttack : Node
{
	private PlayerManager pm;
	public PlayerAttack(PlayerManager _pm)
	{
		pm = _pm;
	}

	public override NodeState Evaluate()
	{
	
		if (Input.GetKeyDown(KeyCode.Q))
		{
			RunAsync().Forget();
			return NodeState.Success;
		}
		return NodeState.Failure;
	}
	private async UniTaskVoid RunAsync()
	{
		//bool next1 = false;
		//bool next2 = false;
		//bool next3 = false;

		//Debug.Log("����1");
		//CancellationTokenSource cts = new CancellationTokenSource();

		//// Q Ű�� ���� ������ ��ٸ�
		//// ���� �ð�(���⼭�� 5��)�� ������ ����մϴ�.
		//var delayTask = UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: cts.Token);
		//Debug.Log("asdasd");

		await UniTask.Delay(TimeSpan.FromSeconds(1));
		Debug.Log("1��");

	}
}
