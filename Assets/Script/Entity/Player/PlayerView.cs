using BehaviorTree;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : Node
{
	public PlayerManager pm;
	List<Collider> coll = new List<Collider>();
	public PlayerView(PlayerManager _pm)
	{
		pm = _pm;
		coll.Clear();

	}

	public override NodeState Evaluate()
	{
		coll.Clear();
		Vector3 myPos = pm.transform.position + Vector3.up * 0.5f;
		Collider[] tragets = Physics.OverlapSphere(myPos, pm.viewRadius, pm.targetMask);

		float lookingAngle = pm.transform.eulerAngles.y;
		//Vector3 rightDir = AngleToDir(pm.transform.eulerAngles.y + pm.viewAngle * 0.5f);
		//Vector3 leftDir = AngleToDir(pm.transform.eulerAngles.y - pm.viewAngle * 0.5f);
		Vector3 lookDir = AngleToDir(lookingAngle);

		if (tragets.Length == 0)
			return NodeState.Failure;
		foreach (Collider enemyColl in tragets)
		{
			Vector3 targetPos = enemyColl.transform.position;
			Vector3 targetDir = (targetPos - myPos).normalized;
			//float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

			//if (targetAngle <= pm.viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, pm.viewRadius, pm.obstacleMask))
			//{
			//	coll.Add(enemyColl);
			//	Debug.DrawLine(myPos, targetPos, Color.red);
			//}

		}
		return NodeState.Success;
	}

	private Vector3 AngleToDir(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
	}
}
