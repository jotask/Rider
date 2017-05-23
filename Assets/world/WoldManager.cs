using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoldManager : MonoBehaviour {

	//public Coin coin;

	const int size = 4;

	public Transform player;

	List<Chunk> chunks;

	//List<Coin> coins;

	public Chunk prefab;

	private int left;
	private int right;

	void Start(){

		this.chunks = new List<Chunk> ();
		//this.coins = new List<Coin> ();

		this.left = 0;
		this.right = 0;
		for(int i = 0; i < size; i++){
			Chunk obj = Instantiate (prefab) as Chunk;
			obj.transform.position = new Vector3 ( i * Chunk.size, 0, 0);
			obj.reset (this.right++);
			this.chunks.Add (obj);
			//this.inst (obj);
		}

	}

	void Update(){
		foreach(Chunk c in this.chunks){
			float dst = calculate (c);
			if(dst > Chunk.size * (size / 2)){
				c.reset (this.right);
				//inst (c);
				this.right++;
				this.left++;
			}else if(dst < -Chunk.size * (size / 2)){
				this.left--;
				this.right--;
				c.reset (this.left);
				//inst (c);
			}
		}
	}

	private float calculate(Chunk c){
		Vector3 a = player.transform.position;
		Vector3 b = c.transform.position;
		return (a.x - b.x);
	}

	//private void inst(Chunk chunk){
		//Vector3[] vert = chunk.vertices;
		//Vector3 c = chunk.transform.position;
		//for(int i = 0; i < vert.Length; i += 3){

			//Vector3 v = vert [i];
			//float x = c.x + v.x; 
			//float y = c.y + v.y + Coin.offset;

			//float value = Mathf.PerlinNoise(x, y);

			//if(value > .65f){
				//Coin co = AddCoin (x, y);
				//co.transform.parent = chunk.transform;
			//}
		//}

		//while(this.coins.Count > 10){
			//Coin obj = this.coins [0];
			//DeleteCoin (obj);
		//}
	//}

	//private Coin AddCoin(float x, float y){
		//Coin obj = Instantiate (coin) as Coin;
		//obj.transform.position = new Vector3 (x, y, 2f);
		//this.coins.Add (obj);
		//return obj;
	//}

	//public void DeleteCoin(Coin coin){
		//int index = this.coins.IndexOf (coin);
		//this.coins.RemoveAt (index);
		//Destroy (coin.gameObject);
	
}