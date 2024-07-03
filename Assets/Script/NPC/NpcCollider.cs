using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollider : MonoBehaviour
{
	private NpcManager npcManager;
	private void Awake()
	{
		npcManager = GetComponentInParent<NpcManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			npcManager.worldSpaceCanvas.gameObject.SetActive(true);
			npcManager.isDialog = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			npcManager.worldSpaceCanvas.gameObject.SetActive(false);
			npcManager.isDialog = false;
		}
	}
}
