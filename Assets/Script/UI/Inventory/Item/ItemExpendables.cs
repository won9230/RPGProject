using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Expendables", menuName = "Item Creation/Expendables")]
public class ItemExpendables : Item, IExpendables
{
	public int amount;
	public void Use()
	{
		amount--;
	}
}
