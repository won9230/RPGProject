using UnityEngine;
using BehaviorTree;


public class PlayerMove : Node
{
	private PlayerManager pm;
	public PlayerMove(PlayerManager _pm)
	{
		pm = _pm;
	}

	public override NodeState FixEvaluate()
	{
		foreach (KeyCode key in pm.keys)
		{
			if (Input.GetKeyDown(key))
			{
				state = NodeState.Failure;
				return state;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			state = NodeState.Failure;
			return state;
		}

		float _moveX = Input.GetAxisRaw("Horizontal");
		float _moveZ = Input.GetAxisRaw("Vertical");
		Vector3 _moveH = pm.transform.right * _moveX;
		Vector3 _moveV = pm.transform.forward * _moveZ;

		Vector3 velocity = (_moveH + _moveV).normalized * pm.moveSpeed;
		//pm.rb.MovePosition(pm.rb.position + velocity * Time.deltaTime);

		//======================================================================================================
		//���ǵ�
		float _targetSpeed = Input.GetKey(KeyCode.LeftShift) ? pm.sprintSpeed : pm.moveSpeed;

		//�Է��� ������ �ӵ� 0
		if (_moveX == 0 && _moveZ == 0) _targetSpeed = 0.0f;
		//�÷��̾��� ���� �ӵ�
		float _currentHorizontalSpeed = new Vector3(pm.controller.velocity.x, 0.0f, pm.controller.velocity.z).magnitude;
		float _speedOffset = 0.1f;
		//float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f

		// �÷��̾� �ӵ� ����
		if (_currentHorizontalSpeed < _targetSpeed - _speedOffset ||
			_currentHorizontalSpeed > _targetSpeed + _speedOffset)
		{
			// ���� �������� �ӵ� ��ȭ�� �ִ� �������� ����� �ƴ� ����� ����� �������ϴ�
			// Lerp�� T�� Ŭ������ �����Ǿ� �����Ƿ� �ӵ��� Ŭ������ �ʿ䰡 �����ϴ�
			pm.speed = Mathf.Lerp(_currentHorizontalSpeed, _targetSpeed * 1f,
				Time.fixedDeltaTime * pm.speedChangeRate);

			// �Ҽ��� 3�ڸ��Ʒ��� ����
			pm.speed = Mathf.Round(pm.speed * 1000f) / 1000f;
		}
		else
		{
			pm.speed = _targetSpeed; 
		}

		pm.animationBlend = Mathf.Lerp(pm.animationBlend, _targetSpeed, Time.deltaTime * pm.speedChangeRate);
		if (pm.animationBlend < 0.01f) pm.animationBlend = 0f;

		// normalise input direction
		Vector3 inputDirection = new Vector3(_moveX, 0.0f, _moveZ).normalized;

		if (_moveX != 0 || _moveZ != 0)
		{
			pm.targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + pm.mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(pm.transform.eulerAngles.y, pm.targetRotation, ref pm.rotationVelocity,
				pm.rotationSmoothTime);

			// ī�޶� �����ǿ� ���� ������.
			pm.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}

		Vector3 targetDirection = Quaternion.Euler(0.0f, pm.targetRotation, 0.0f) * Vector3.forward;

		pm.controller.Move(targetDirection.normalized * (pm.speed * Time.fixedDeltaTime) +
				 new Vector3(0.0f, pm.verticalVelocity, 0.0f) * Time.fixedDeltaTime);

		// update animator if using character
		if (pm.hasAnimator)
		{
			pm.animator.SetFloat(pm.animIDSpeed, pm.animationBlend);
		}

		state = NodeState.Success;
		//Debug.Log($"state {state}");

		return state;
	}

}
