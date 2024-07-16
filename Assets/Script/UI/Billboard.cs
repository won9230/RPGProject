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
		// 오브젝트가 카메라를 바라보도록 설정
		Vector3 direction = targetCamera.position - transform.position;
		direction.y = 0; // y축 회전을 막아 수직 축을 고정
		transform.rotation = Quaternion.LookRotation(direction);
	}
}
