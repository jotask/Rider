using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Vector2 offset = new Vector2 ();

	public float speed = 1;

	private float z = 0;

	Transform player;

	// Use this for initialization
	void Awake () {
		this.z = this.transform.position.z;
		this.player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 tmp = new Vector2(this.player.position.x, this.player.position.y) + this.offset;
		Vector2 pos = Vector2.Lerp (this.transform.position, tmp, Time.deltaTime * this.speed);
		this.transform.position = new Vector3 (pos.x, pos.y, z);
	}
}
