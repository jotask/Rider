using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

	public Players[] players;

	public GameObject frontWheel;
	public GameObject backWheel;

	public int player;

	public bool desktop;
	public bool invecible;
	
	private GameController gameController;

	private Motor motor;

	void Awake()
	{
		
		player = PlayerPrefs.GetInt("player", 0);
		
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		Players cfg = players[player];
		
		GetComponent<SpriteRenderer>().sprite = cfg.carBody;
		gameObject.AddComponent<PolygonCollider2D>();
		
		setWheel(cfg, frontWheel);
		setWheel(cfg, backWheel);

		gameObject.transform.localScale = new Vector3(cfg.scale, cfg.scale, cfg.scale);
		
		motor = GetComponent<Motor>();

	}

	private void setWheel(Players cfg, GameObject obj)
	{
		obj.GetComponent<CircleCollider2D>().radius = cfg.wheelRadious;
		obj.GetComponent<SpriteRenderer>().sprite = cfg.wheel;
		obj.GetComponent<SpriteRenderer>().sortingOrder = cfg.wheelRendererLayer;
		obj.transform.localScale = Vector3.one * cfg.wheelScale;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;
		if(tag == "Coin"){
			AudioManager.instance.PlaySound2D("coin");
			gameController.AddScore(other.GetComponent<Coin> ().getValue ());
			Destroy (other.gameObject);
		}else if (tag == "Fuel")
		{
			motor.fuel += 1f;
			Destroy(other.gameObject);
		}
	}

}
