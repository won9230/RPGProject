using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcManager : MonoBehaviour
{
	//NPC다이얼로그 체크 UI오브젝트
	public GameObject dialogUI;
	//UI오브젝트를 넣어줄 캔버스
	[HideInInspector] public GameObject worldSpaceCanvas;
	[HideInInspector] public bool isDialog = false;

	private void Awake()
	{
		//NPC DialogUI 비활성화
		worldSpaceCanvas = this.transform.Find("World Space Canvas").gameObject;
		worldSpaceCanvas.gameObject.SetActive(false);
		if (worldSpaceCanvas == null)
			Debug.Log("worldSpaceCanvas를 찾지 못함");
	}

}