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
			PlayerManager pm = other.GetComponent<PlayerManager>();
			npcManager.worldSpaceCanvas.gameObject.SetActive(true);
			pm.inNpc = npcManager;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerManager pm = other.GetComponent<PlayerManager>();
			npcManager.worldSpaceCanvas.gameObject.SetActive(false);
			pm.inNpc = null;
		}
	}
}
