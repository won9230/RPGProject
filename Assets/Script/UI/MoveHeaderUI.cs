using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
	[SerializeField] private Transform target;

	private Vector2 beginPont;
	private Vector2 moveBegin;

	public void OnPointerDown(PointerEventData eventData)
	{
		beginPont = target.position;
		moveBegin = eventData.position;
	}
	public void OnDrag(PointerEventData eventData)
	{
		target.position = beginPont + (eventData.position - moveBegin);
	}
}
