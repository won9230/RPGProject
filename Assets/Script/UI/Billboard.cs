using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Transform targetCamera;

	private void Start()
	{
		targetCamera = GameObject.Find("Main Camera").transform;
	}

	void Update()
	{
		// ������Ʈ�� ī�޶� �ٶ󺸵��� ����
		Vector3 direction = targetCamera.position - transform.position;
		direction.y = 0; // y�� ȸ���� ���� ���� ���� ����
		transform.rotation = Quaternion.LookRotation(direction);
	}
}
