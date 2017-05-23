using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoldManager : MonoBehaviour {

	//public Coin coin;

	const int size = 4;

	private Transform player;

	List<Chunk> chunks;

	public Chunk prefab;

	private int left;
	private int right;

	void Start(){

		this.player = GameObject.FindGameObjectWithTag ("Player").transform;

		this.chunks = new List<Chunk> ();

		int initial = 1;

		this.left = initial;
		this.right = initial;

		for(int i = 0; i < size; i++){
			Chunk obj = Instantiate (prefab) as Chunk;
			obj.transform.parent = this.transform;
			obj.transform.position = new Vector3 ( i * Chunk.size, 0, 0);
			obj.reset (this.right++);
			this.chunks.Add (obj);
		}

	}

	void Update(){
		foreach(Chunk c in this.chunks){
			float dst = calculate (c);
			if(dst > Chunk.size * (size / 2)){
				c.reset (this.right);
				this.right++;
				this.left++;
			}else if(dst < -Chunk.size * (size / 2)){
				this.left--;
				this.right--;
				c.reset (this.left);
			}
		}
	}

	private float calculate(Chunk c){
		Vector3 a = player.transform.position;
		Vector3 b = c.transform.position;
		return (a.x - b.x);
	}
	
}