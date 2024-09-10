using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		if(HasItem())
		{
			GameObject dropped = eventData.pointerDrag;
			DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
			draggableItem.parentAfterDrag = transform;
		}
	}

	private bool HasItem() => transform.childCount <= 0;
}
