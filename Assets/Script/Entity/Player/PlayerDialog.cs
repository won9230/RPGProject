using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog : Node
{
	private PlayerManager pm;

	public PlayerDialog(PlayerManager _pm)
	{
		pm = _pm;
	}

	public override NodeState Evaluate()
	{
		if(Input.GetKeyUp(KeyCode.F)) 
		{
			if(DialogManager.instance.cntId == -1) 
			{
				DialogManager.instance.StartDialog(0);
				pm.isDialog = true;
			}
			else
			{
				if (DialogManager.instance.NextDialog()) 
				{
					pm.isDialog = false;
				}
			}
		}
		if (pm.isDialog)
		{
			state = NodeState.Success;
			return state;
		}
		state = NodeState.Failure;
		return state;
	}
}
