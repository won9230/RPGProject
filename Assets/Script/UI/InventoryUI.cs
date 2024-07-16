using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

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
	private void Start()
	{
		InitSlot();

	}
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

	public void CloseButton()
	{
		Debug.Log("CloseButton method called.");

		if (inventoryUI != null)
		{
			Debug.Log("inventoryUI is assigned.");
			Debug.Log($"inventoryUI active state before: {inventoryUI.activeSelf}");

			inventoryUI.SetActive(false);

			Debug.Log($"inventoryUI active state after: {inventoryUI.activeSelf}");
			Debug.Log("inventoryUI has been set to inactive.");
		}
		else
		{
			Debug.LogError("inventoryUI is not assigned in the inspector.");
		}
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
}
