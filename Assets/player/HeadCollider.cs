using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{

	private Hud hud;

	void Start ()
	{
		hud = GameObject.FindGameObjectWithTag("Ui").GetComponent<Hud>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		hud.GameOver();
	}
	
}
