using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
	//아이템 이미지
	[SerializeField] private Image iconImage;
	//아이템 수량
	[SerializeField] private Text amountText;
	//하이라이트 이미지
	[SerializeField] private Image highlightImage;
	//하이라이트 이미지 알파값
	[SerializeField] private float highlightAlpha = 0.5f;
	//하이라이트 소요시간
	[SerializeField] private float highlightFadeDuration = 0.2f;
	//슬롯 접근가능 여부
	private bool isAccessibleSlot = true;
	//아이템 접근가능 여부
	private bool isAccessibleItem = true;

	private InventoryUI inventoryUI;

	private RectTransform slotRect;
	private RectTransform iconRect;
	private RectTransform highlightRect;

	private GameObject iconGo;
	private GameObject textGo;
	private GameObject highlightGo;

	private Image slotImage;

	private float currentHlightAlpha = 0f;

	private static readonly Color inSlotColor = new Color(0.2f,0.2f,0.2f);
	private static readonly Color inIconColor = new Color(0.5f,0.5f,0.5f);
	public int Index { set; get; }
	public bool HasItem => iconImage.sprite != null;
	public bool IsAccessible => isAccessibleSlot && isAccessibleItem;

	public RectTransform SlotRect => slotRect;
	public RectTransform IconRect => IconRect;
	
	private void ShowIcon() => iconGo.SetActive(true);
	private void HideIcon() => iconGo.SetActive(false);
	private void ShowText() => textGo.SetActive(true);
	private void HideText() => textGo.SetActive(false);

	public void SetSlotIndex(int _index) => Index = _index;

	public void SetSlotState(bool _value)
	{
		if(isAccessibleSlot == _value) return;

		if(_value)
		{
			slotImage.color = Color.black;
		}
		else
		{
			slotImage.color = inSlotColor;
			HideIcon();
			HideText();
		}

		isAccessibleSlot = _value;
	}

	public void SetItemState(bool _value)
	{
		if(isAccessibleItem == _value)
			return;

		if(_value)
		{
			iconImage.color = Color.white;
			amountText.color = Color.white;
		}
		else
		{
			iconImage.color = inIconColor;
			amountText.color = inIconColor;
		}

		isAccessibleItem = _value;
	}

	public void SwapOrMoveIcon(ItemSlotUI _slotUI)
	{
		if(_slotUI == null) return;
		if(_slotUI == this) return;
		if(!this.IsAccessible) return;
		if(!_slotUI.IsAccessible) return;

		var tmp = iconImage.sprite;

		if(_slotUI.HasItem)
		{
			SetItem(_slotUI.iconImage.sprite);
		}
		else
		{
			RemoveItem();
		}

		_slotUI.SetItem(tmp);
	}

	public void SetItem(Sprite _itemSprite)
	{
		if(_itemSprite != null)
		{
			iconImage.sprite = _itemSprite;
			ShowIcon();
		}
		else
		{
			RemoveItem();
		}
	}
	public void RemoveItem()
	{
		iconImage.sprite = null;
		HideIcon();
		HideText();
	}

	public void SetIconAlpha(float _alpha)
	{
		iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, _alpha);
	}

	public void SetItemAmount(int _amount)
	{
		if(HasItem && _amount > 1)
		{
			ShowText();
		}
		else
		{
			HideText();
		}

		amountText.text = _amount.ToString();
	}
}
