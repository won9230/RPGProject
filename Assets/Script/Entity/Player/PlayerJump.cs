using BehaviorTree;
using UnityEngine;

public class PlayerJump : Node
{
	private PlayerManager pm;
	public PlayerJump(PlayerManager _pm)
	{
		pm = _pm;
	}
	public override NodeState Evaluate()
	{
		//Ground를 체크합니다.
		pm.isGround = Physics.Raycast(pm.transform.position, Vector3.down, pm.groundCheck, pm.groundLayer);
		//플레이어 점프
		if (pm.isGround)
		{
			//애니메이션 관련
			if (pm.hasAnimator)
			{
				pm.animator.SetBool(pm.animIDJump, false);
			}

			if (pm.verticalVelocity < 0.0f)
			{
				pm.verticalVelocity = -2f;
			}

			if (Input.GetKeyDown(KeyCode.Space) && pm.jumpTimeoutDelta <= 0.0f)
			{
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				pm.verticalVelocity = Mathf.Sqrt(pm.jumpHeight * -2f * pm.gravity);

				// update animator if using character
				if (pm.hasAnimator)
				{
					pm.animator.SetBool(pm.animIDJump, true);
				}
			}
			if (pm.jumpTimeoutDelta >= 0.0f)
			{
				pm.jumpTimeoutDelta -= Time.deltaTime;
			}
			state = NodeState.Failure;
		}
		else
		{
			// reset the jump timeout timer
			pm.jumpTimeoutDelta = pm.jumpTimeout;

			// fall timeout
			//if (pm.fallTimeoutDelta >= 0.0f)
			//{
			//	pm.fallTimeoutDelta -= Time.deltaTime;
			//}

			state = NodeState.Success;
		}

		if (pm.verticalVelocity < pm.terminalVelocity)
		{
			pm.verticalVelocity += pm.gravity * Time.deltaTime;
		}
		return state;
	}
}
