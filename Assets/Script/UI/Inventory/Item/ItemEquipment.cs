using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Equipment
{
	Armor,
	Weapon,
}

[CreateAssetMenu(fileName = "Item_Equipment", menuName = "Item Creation/Equipment")]
public class ItemEquipmentI : Item, IEquipment
{
	public Equipment equipment;
	public void Equip()
	{

	}
}
