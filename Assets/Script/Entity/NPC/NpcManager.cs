using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class NpcManager : MonoBehaviour
{
	//NPC다이얼로그 체크 UI오브젝트
	public GameObject dialogUI;
	public bool isdialog = false;
	//UI오브젝트를 넣어줄 캔버스
	[HideInInspector] public GameObject worldSpaceCanvas;
	//카메라 오브젝트
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