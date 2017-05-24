using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int value = 1;

	[Range (0f, 1f)]
	public float offset = .5f;

	private float timingOffset;

	private Vector3 p;

	void Awake () {
		this.p = this.transform.position;
		this.timingOffset = Random.value * (Mathf.PI / 2);
		Destroy (this.gameObject, 10f);
	}

	void Update () {
		transform.position = p + new Vector3 (0f, Mathf.Sin(Time.time + this.timingOffset) * offset, 0f);
		transform.localScale = new Vector3(Mathf.Cos(Time.time + this.timingOffset), 1f, 1f);
	}

	void OnValidate(){
		if(offset <= 0){
			offset = 0.001f;
		}
	}

	public int getValue(){
		return this.value;
	}

}
