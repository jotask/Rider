using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tricks : MonoBehaviour
{

	private DegreeTracker tracker;
	private Manual manual;

	public Wheel front;
	public Wheel back;

	// Use this for initialization
	void Start ()
	{
		tracker = gameObject.AddComponent<DegreeTracker>();
		manual = gameObject.AddComponent<Manual>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!front.isGround && !back.isGround)
		{
			if (!tracker.enabled)
				tracker.StartJump();
		}
		else if (front.isGround && !back.isGround){
			if (!manual.enabled)
			{
				manual.ManualStart();
			}
		}else if (!front.isGround && !back.isGround)
		{
			if (!manual.enabled)
			{
				manual.ManualStart();
			}
		}

		if (front.isGround && back.isGround)
		{
			manual.enabled = false;
		}

	}

	public void frontFlip()
	{
		Debug.Log("FrontFlip");
	}

	public void backFlip()
	{
		Debug.Log("BackFlip");
	}
	
	private void OnGUI()
	{
		Vector3 offset = new Vector3(0, -2f);
		Vector3 p = UnityEngine.Camera.main.WorldToScreenPoint(transform.position + offset);
		GUI.Label(new Rect(p.x, p.y, 200, 500), "Hi Jose");
	}
	
}
