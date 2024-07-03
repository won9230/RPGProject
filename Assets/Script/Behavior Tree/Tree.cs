using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
	public abstract class Tree : MonoBehaviour
	{
		private Node _root = null;

		protected void Start()
		{
			OnEnter();
			_root = SetupTree();
		}

		private void Update()
		{
			if (_root != null)
			{
				_root.Evaluate();
			}
		}
		private void FixedUpdate()
		{
			if(_root != null )
			{
				_root.FixEvaluate();
			}
		}
		protected abstract Node SetupTree();
		protected abstract void OnEnter();
	}
}
