using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Node
{
	private PlayerManager pm;
	public PlayerInventory(PlayerManager _pm)
	{
		pm = _pm;
	}

	public override NodeState Evaluate()
	{
		if(Input.GetKeyDown(KeyCode.Tab) && !pm.isDialog)
		{
			pm.isInventory = !pm.isInventory;
		}
		if(pm.isInventory)
		{
			pm.inventory.SetActive(pm.isInventory);
			return NodeState.Success;
		}
		else
		{
			pm.inventory.SetActive(pm.isInventory);
			return NodeState.Failure;
		}
	}
}
