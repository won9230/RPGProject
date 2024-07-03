using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;


namespace BehaviorTree
{
	public class Parallel : Node
	{
		private int successThreshold;
		public Parallel(int _successThreshold) : base() 
		{
			successThreshold = _successThreshold;
		}
		public Parallel(int _successThreshold, List<Node> children) : base(children) 
		{
			successThreshold = _successThreshold;
		}

		public override NodeState Evaluate()
		{
			int successCount = 0;
			int failureCount = 0;

			foreach (Node node in children)
			{
				switch (node.Evaluate())
				{
					case NodeState.Failure:
						failureCount++;
						break;
					case NodeState.Success:
						successCount++;
						break;
					case NodeState.Running:
						break;
				}
			}
			if(successCount >= successThreshold)
				state = NodeState.Success;
			else if(failureCount >= (children.Count - successThreshold))
				state = NodeState.Failure;
			else
				state = NodeState.Running;

			return state;
		}

		public override NodeState FixEvaluate()
		{
			int successCount = 0;
			int failureCount = 0;
			foreach (Node node in children)
			{
				switch (node.FixEvaluate())
				{
					case NodeState.Failure:
						failureCount++;
						break;
					case NodeState.Success:
						successCount++;
						break;
					case NodeState.Running:
						break;
				}
			}
			if (successCount >= successThreshold)
				state = NodeState.Success;
			else if (failureCount >= (children.Count - successThreshold))
				state = NodeState.Failure;
			else
				state = NodeState.Running;

			//Debug.Log($"successCount {successCount}");
			return state;
		}
	}
}

