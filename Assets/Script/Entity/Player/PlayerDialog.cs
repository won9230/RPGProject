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
		if(Input.GetKeyDown(KeyCode.F) && pm.inNpc != null)
		{
			if(DialogManager.instance.cntId == -1 && pm.inNpc != null) 
			{
				pm.inNpc.NpcDialogStart();
				pm.playerCamera.SetActive(false);
				pm.isDialog = true;
			}
			else if(DialogManager.instance.NextDialog())
			{
				pm.inNpc.NpcDialogEnd();
				pm.playerCamera.SetActive(true);
				pm.isDialog = false;
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
