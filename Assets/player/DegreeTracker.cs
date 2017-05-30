using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEditor;
using UnityEngine;
using Camera = UnityEngine.Camera;

public class DegreeTracker : MonoBehaviour
{

	private float start;
	private float amount;
	private float lastAngle;

	private Tricks tricks;

	private Vector3 dir;

	void Start()
	{
		tricks = GetComponent<Tricks>();
		// no tracking by default
		enabled = false;
	} 
 
	// called from somewhere when the tracking begins
	public void StartJump()
	{
		start = transform.eulerAngles.z;
		dir = this.transform.right;
		amount = 0f;
		lastAngle = 0f;
		enabled = true;
	}

	void Update()
	{
		amount += Mathf.Acos(Vector3.Dot(dir, transform.right));
		dir = transform.right;
		Debug.Log(amount);
	}

}