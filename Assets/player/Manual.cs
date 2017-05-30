using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
	private float time;

	// Use this for initialization
	void Start ()
	{
		enabled = false;
	}

	public void ManualStart()
	{
		time = 0f;
		enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		Debug.Log(time);
	}
	
}
