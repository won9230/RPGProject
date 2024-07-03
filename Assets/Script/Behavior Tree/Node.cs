using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine.PlayerLoop;

namespace BehaviorTree
{
	public enum NodeState
	{
		Running,
		Success,
		Failure
	}
	public class Node
	{
		protected NodeState state;

		public Node parent;
		protected List<Node> children = new List<Node>();

		private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

		public Node()
		{
			parent = null;
		}
		public Node(List<Node> children)
		{
            foreach (Node child in children)
            {
				_Attach(child);
			}
        }
		private void _Attach(Node node) 
		{
			node.parent = this;
			children.Add(node);
		}

		public virtual NodeState Evaluate()
		{
			return NodeState.Failure;
		}
		public virtual NodeState FixEvaluate()
		{
			return NodeState.Failure;
		}

		public void SetDate(string key, object value)
		{
			_dataContext[key] = value;
		}

		public object GetDate(string key)
		{
			object value = null;
			if (_dataContext.TryGetValue(key, out value))
				return value;

			Node node = parent;
			while (node != null)
			{
				value = node.GetDate(key);
				if(value != null) 
					return value;	
				node = node.parent;
			}
			return null;
		}
		public bool ClearDate(string key)
		{

			if (_dataContext.ContainsKey(key))
			{
				_dataContext.Remove(key);
				return true;
			}

			Node node = parent;
			while (node != null)
			{
				bool cleard = node.ClearDate(key);
				if (cleard)
					return true;
				node = node.parent;
			}
			return false;
		}
	}

}
