using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class NpcManager : MonoBehaviour
{
	//NPC���̾�α� üũ UI������Ʈ
	public GameObject dialogUI;
	public bool isdialog = false;
	//UI������Ʈ�� �־��� ĵ����
	[HideInInspector] public GameObject worldSpaceCanvas;
	//ī�޶� ������Ʈ
	public GameObject cameraSetting;

	private void Awake()
	{
		//NPC DialogUI ��Ȱ��ȭ
		worldSpaceCanvas = this.transform.Find("World Space Canvas").gameObject;
		worldSpaceCanvas.SetActive(false);
		cameraSetting.SetActive(false);
		if (worldSpaceCanvas == null)
			Debug.Log("worldSpaceCanvas�� ã�� ����");
	}

	public void NpcDialogStart()
	{
		DialogManager.instance.StartDialog(0);
		CameraActive(true);
		worldSpaceCanvas.SetActive(true);
	}

	public void NpcDialogEnd()
	{
		CameraActive(false);
		worldSpaceCanvas.SetActive(true);
	}

	public void CameraActive(bool _set)
	{
		cameraSetting.SetActive(_set);
	}


}