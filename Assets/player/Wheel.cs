using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	
	public bool isGround { get; private set; }

	private void OnCollisionEnter2D(Collision2D other)
	{
		string tag = other.gameObject.tag.ToLower();
		if (tag == "world")
		{
			isGround = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		string tag = other.gameObject.tag.ToLower();
		if (tag == "world")
		{
			isGround = false;
		}
	}
	
}
