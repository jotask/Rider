using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public UnityEngine.UI.Text text;

	private int score;

	void Start	()
	{
		this.score = 0;
	}

	

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Coin"){
			AudioManager.instance.PlaySound2D("coin");
			this.score += other.GetComponent<Coin> ().getValue ();
			this.text.text = "Score: " + this.score;
			Destroy (other.gameObject);
		}
	}

}
