using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
	public int id;
	public new string name;
	[Multiline]
	public string toolTip;
	public Sprite iconSprite;
}
