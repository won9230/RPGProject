using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float minDistance = 3;     
	[SerializeField] private float maxDistance = 7;     
	[SerializeField] private float wheelSpeed = 500;
	[SerializeField] private float xMoveSpeed = 150;
	[SerializeField] private float yMoveSpeed = 100;
	[SerializeField] private float yOffset = 1.5f;
	public LayerMask collisionLayers;
	private float yMinLimit = 5;
	private float yMaxLimit = 80;
	private float x, y;
	private float distance;

	private void Start()
	{
		distance = Vector3.Distance(transform.position, target.position);
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
	}

	private void FixedUpdate()
	{
		if (target == null)
		{
			Debug.Log("타겟이 없습니다.");
			return;
		}

		x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
		y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

		y = ClampAngele(y, yMinLimit, yMaxLimit);

		transform.rotation = Quaternion.Euler(y, x, 0).normalized;
		distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
	}
	private void LateUpdate()
	{
		transform.position = transform.rotation * new Vector3(0, yOffset, -distance) + target.position;
	}

	private float ClampAngele(float angle, float min, float max)
	{
		if (angle < -360)
		{
			angle += 360;
		}
		if (angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp(angle, min, max);
	}


}

