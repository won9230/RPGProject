using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcManager : MonoBehaviour
{
	//NPC���̾�α� üũ UI������Ʈ
	public GameObject dialogUI;
	//UI������Ʈ�� �־��� ĵ����
	[HideInInspector] public GameObject worldSpaceCanvas;
	[HideInInspector] public bool isDialog = false;

	private void Awake()
	{
		//NPC DialogUI ��Ȱ��ȭ
		worldSpaceCanvas = this.transform.Find("World Space Canvas").gameObject;
		worldSpaceCanvas.gameObject.SetActive(false);
		if (worldSpaceCanvas == null)
			Debug.Log("worldSpaceCanvas�� ã�� ����");
	}

}