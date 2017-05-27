using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Inventory/List", order = 1)]
public class Players : ScriptableObject
{

	public string name;

	public float scale;
	
	public Sprite carBody;
	public Sprite wheel;
	public Sprite head;
	
	public float wheelRadious;

	public float wheelScale;
	public int wheelRendererLayer;
	
	public Vector2 backAnchor;
	public Vector2 frontAnchor;
	
	public Vector2 headAnchor;
	public Vector2 connectedAnchorHead;


}