using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
	public int Index { set; get; }
	
	public void SetSlotIndex(int slotIndex)
	{
		Index = slotIndex;
	}
}
