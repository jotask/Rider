using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int value = 1;

	[Range (0f, 1f)]
	public float offset = .5f;

	private Vector3 p;

	void Awake () {
		this.p = this.transform.position;
		Destroy (this.gameObject, 10f);
	}

	void Update () {
		transform.position = p + new Vector3 (0f, Mathf.Sin(Time.time) * offset, 0f);
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
