using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
	[Header("옵션")]
	[Range(0f, 15f)]
	[SerializeField] private int horizontalSlotCount = 8;
	[Range(0f, 15f)]
	[SerializeField] private int verticalSlotCount = 8;
	[SerializeField] private float slotMargin = 8f;
	[SerializeField] private float contentHAreaPadding = 20f;
	[SerializeField] private float contentVAreaPadding = 20f;
	[Range(30, 100)]
	[SerializeField] private float slotSize = 64f;

	[Header("오브젝트")]
	[SerializeField] private RectTransform contentAreaRT;
	[SerializeField] private GameObject slotUiPrefab;
	[SerializeField] private GameObject inventoryUI;

	List<ItemSlotUI> slotUIList = new List<ItemSlotUI>();

	//마우스 클릭
	private GraphicRaycaster gr;
	private PointerEventData ped;
	private List<RaycastResult> rrList;

	private ItemSlotUI dragSlot;	//드래그 시작한 슬록
	private Transform dragIconTransform;	//해당 슬롯 아이콘의 Transform

	private Vector3 dragIconPoint;	//드래그 시작 시 슬롯 위치
	private Vector3 dragCursorPoint;	//드랙 시작 시 커서의 위치
	private int dragSlotSiblingIndex;
	private void Start()
	{
		InitSlot();

	}
	private void Update()
	{
		ped.position = Input.mousePosition;

		OnPointerDown();
		OnPointerDrag();
		OnPointerUp();
	}
	/// <summary>
	/// 슬롯 생성
	/// </summary>
	private void InitSlot()
	{
		slotUiPrefab.TryGetComponent(out RectTransform slotRect);
		slotRect.sizeDelta = new Vector2 (slotSize, slotSize);


		slotUiPrefab.TryGetComponent(out ItemSlotUI itemSlot);
		if(itemSlot == null)
			slotUiPrefab.AddComponent<ItemSlotUI>();

		slotUiPrefab.SetActive(false);

		Vector2 beginPos = new Vector2(contentHAreaPadding, -contentVAreaPadding);
		Vector2 curPos = beginPos;

		slotUIList = new List<ItemSlotUI>(verticalSlotCount * horizontalSlotCount);

        for (int i = 0; i < verticalSlotCount; i++)
        {
            for (int j = 0; j < horizontalSlotCount; j++)
            {
				int slotIndex = (horizontalSlotCount * j) + i;

				var slotRT = CloneSlot();
				slotRT.pivot = new Vector2(0f, 1f);
				slotRT.anchoredPosition = curPos;
				slotRT.gameObject.SetActive(true);
				slotRT.gameObject.name = $"Item Slot [{slotIndex}]";
				//slotRT.transform.localScale = Vector3.one; // ���� �ذ�

				var slotUI = slotRT.GetComponent<ItemSlotUI>();
				slotUI.SetSlotIndex(slotIndex);
				slotUIList.Add(slotUI);

				curPos.x += (slotMargin + slotSize);
			}

			curPos.x = beginPos.x;
			curPos.y -= (slotMargin + slotSize);
        }

		if(slotUiPrefab.scene.rootCount != 0)
		{
			Destroy(slotUiPrefab);
		}

		inventoryUI.SetActive(false);
	}

	private RectTransform CloneSlot()
	{
		GameObject slotPrefab = Instantiate(slotUiPrefab);
		RectTransform rt = slotPrefab.GetComponent<RectTransform>();
		rt.SetParent(contentAreaRT);

		return rt;
	}

#if UNITY_EDITOR
	[SerializeField] private bool showPreview = false;
	[SerializeField] private List<GameObject> slotsPreviewList = new List<GameObject>();
	private int horizontalSlotCountPreview = 0;
	private int verticalSlotCountPreview = 0;
	private void OnValidate()
	{
		

		if (Application.isPlaying) return;

		if(horizontalSlotCount != horizontalSlotCountPreview ||
			verticalSlotCount != verticalSlotCountPreview)
		{
			ClearAll();
			horizontalSlotCountPreview = horizontalSlotCount;
			verticalSlotCountPreview = verticalSlotCount;
		}

		if (showPreview)
		{
			CreateSlots();
		}
		else
		{
			ClearAll();
		}

		void CreateSlots()
		{
			ClearAll();
			int count = horizontalSlotCount * verticalSlotCount;

			RectTransform slotPrefabRT = slotUiPrefab.GetComponent<RectTransform>();
			slotPrefabRT.pivot = new Vector2(0f, 1f);
			slotPrefabRT.sizeDelta = new Vector2(slotSize, slotSize);

			for (int i = 0; i < count; i++)
			{
				GameObject slotGo = Instantiate(slotUiPrefab);
				slotGo.transform.SetParent(contentAreaRT.transform);
				slotGo.SetActive(true);

				//slotGo.transform.localScale = Vector3.one; // ���� �ذ�
				slotsPreviewList.Add(slotGo);
			}
			DrawGrid();
		}

		void DrawGrid()
		{
			Vector2 beginPos = new Vector2(contentHAreaPadding, -contentVAreaPadding);
			Vector2 curPos = beginPos;

			int index = 0;

			for (int i = 0; i < verticalSlotCount; i++)
			{
				for (int j = 0; j < horizontalSlotCount; j++)
				{
					GameObject slotGo = slotsPreviewList[index++];

					RectTransform slotRT = slotGo.GetComponent<RectTransform>();
					slotRT.pivot = new Vector2(0f, 1f);
					slotRT.anchoredPosition = curPos;
					slotRT.gameObject.SetActive(true);

					curPos.x += (slotMargin + slotSize);
				}

				curPos.x = beginPos.x;
				curPos.y -= (slotMargin + slotSize);
			}
		}

		void ClearAll()
		{
            foreach (var item in slotsPreviewList)
            {
				Destroyer.Destroy(item);
            }
			slotsPreviewList.Clear();
        }


	}

	[UnityEditor.InitializeOnLoad]
	private static class Destroyer
	{
		private static Queue<GameObject> targetQueue = new Queue<GameObject>();

		static Destroyer()
		{
			UnityEditor.EditorApplication.update += () =>
			{
				for (int i = 0; targetQueue.Count > 0 && i < 100000; i++)
				{
					var next = targetQueue.Dequeue();
					DestroyImmediate(next);
				}
			};
		}
		public static void Destroy(GameObject go) => targetQueue.Enqueue(go);
	}
#endif

	private T RaycastAndGetFirstComponent<T>() where T : Component
	{
		rrList.Clear();

		gr.Raycast(ped, rrList);

		if(rrList.Count == 0)
			return null;

		return rrList[0].gameObject.GetComponent<T>();
	}

	private void OnPointerDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			dragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

			if(dragSlot != null && dragSlot.HasItem)
			{
				//위치 기억
				dragIconTransform = dragSlot.IconRect.transform;
				dragIconPoint = dragIconTransform.position;
				dragCursorPoint = Input.mousePosition;

				dragSlotSiblingIndex = dragSlot.transform.GetSiblingIndex();
				dragSlot.transform.SetAsLastSibling();

				// dragSlot.SetHighlightOnTop(false);
			}
			else
			{
				dragSlot = null;
			}
		}
	}

	private void OnPointerDrag()
	{
		if(dragSlot == null) 
			return;
		
		if(Input.GetMouseButton(0))
		{
			dragIconTransform.position = dragIconPoint + (Input.mousePosition - dragCursorPoint);
		}
	}

	private void OnPointerUp()
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(dragSlot != null)
			{
				dragIconTransform.position = dragIconPoint;

				dragSlot.transform.SetSiblingIndex(dragSlotSiblingIndex);

				EndDrag();

				dragSlot = null;
				dragIconTransform = null;
			}
		}
	}

	private void EndDrag()
	{
		ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
	}
}
