using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public Players[] players;

	public GameObject frontWheel;
	public GameObject backWheel;

	public PhysicsMaterial2D bodyMaterial;
	public PhysicsMaterial2D wheelMaterial;

	public int player;

	public bool desktop;
	public bool invecible;
	
	private GameController gameController;

	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		Players cfg = players[player];
		
		GetComponent<SpriteRenderer>().sprite = cfg.carBody;
		gameObject.AddComponent<PolygonCollider2D>();
		
		setWheel(cfg, frontWheel);
		setWheel(cfg, backWheel);

		gameObject.transform.localScale = new Vector3(cfg.scale, cfg.scale, cfg.scale);

	}

	private void setWheel(Players cfg, GameObject obj)
	{
		obj.GetComponent<CircleCollider2D>().radius = cfg.wheelRadious;
		obj.GetComponent<SpriteRenderer>().sprite = cfg.wheel;
		obj.GetComponent<SpriteRenderer>().sortingOrder = cfg.wheelRendererLayer;
		obj.transform.localScale = Vector3.one * cfg.wheelScale;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Coin"){
			AudioManager.instance.PlaySound2D("coin");
			gameController.AddScore(other.GetComponent<Coin> ().getValue ());
			Destroy (other.gameObject);
		}
	}

}
