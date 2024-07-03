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

		//Debug.Log("공격1");
		//CancellationTokenSource cts = new CancellationTokenSource();

		//// Q 키가 눌릴 때까지 기다림
		//// 일정 시간(여기서는 5초)이 지나면 취소합니다.
		//var delayTask = UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: cts.Token);
		//Debug.Log("asdasd");

		await UniTask.Delay(TimeSpan.FromSeconds(1));
		Debug.Log("1초");

	}
}
