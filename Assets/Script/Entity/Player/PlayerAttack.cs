using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using BehaviorTree;
using System.Threading;
using System;

public class PlayerAttack : Node
{
	private PlayerManager pm;
	private CancellationTokenSource _cancellationTokenSource;
	private string[] attackAnimNames = { "ComboAttack_1", "ComboAttack_2", "ComboAttack_3" };
	public PlayerAttack(PlayerManager _pm)
	{
		pm = _pm;
	}

	public override NodeState Evaluate()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			pm.animator.SetTrigger(pm.animIDAttack);
		}
		if (IsAnimPlaying(attackAnimNames))
			pm.isAttack = true;
		else
			pm.isAttack = false;

		if(pm.isAttack)
		{
			state = NodeState.Success;
			return state;
		}
		state = NodeState.Failure;
		return state;
	}

	private bool IsAnimPlaying(string[] _attackNames)
	{
		AnimatorStateInfo animStateInfo = pm.animator.GetCurrentAnimatorStateInfo(0);

        for (int i = 0; i < _attackNames.Length; i++)
        {
			if (animStateInfo.IsName(_attackNames[i]))
			{
				return true;
			}
        }

        return false;
	}

	private async UniTaskVoid StartAttackTask()
	{
		_cancellationTokenSource = new CancellationTokenSource();
		try
		{
			//await LoadWithProgressAsync(_cancellationTokenSource.Token);
			pm.animator.SetTrigger(pm.animIDAttack);
			float animInfoLength = pm.animator.GetCurrentAnimatorStateInfo(0).length;
			
			await UniTask.Delay(10);

		}
		catch (OperationCanceledException)
		{
			Debug.Log("Attack 취소");
		}
		finally
		{
			_cancellationTokenSource.Dispose();
		}
	}



	public void CancelLoading()
	{
		if (_cancellationTokenSource != null)
		{
			_cancellationTokenSource.Cancel();
		}
	}
}
