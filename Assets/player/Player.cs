using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public bool invecible;
	
	private GameController gameController;

	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Coin"){
			AudioManager.instance.PlaySound2D("coin");
			gameController.AddScore(other.GetComponent<Coin> ().getValue ());
			Destroy (other.gameObject);
		}
	}

}
