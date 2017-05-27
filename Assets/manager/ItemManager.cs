using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public enum Items
	{
		COINS, FUEL
	};

	private float coinOffset = .9f;

	private float fuelOffset = .5f;

	public GameObject coin;
	public GameObject fuel;

	void Start()
	{
		coinOffset = PlayerPrefs.GetFloat(Items.COINS.ToString().ToLower(), 1f);
		fuelOffset = PlayerPrefs.GetFloat(Items.FUEL.ToString().ToLower(), 1f);
	}

	public void newChunk(Chunk c){
		Vector3[] vert = c.vertices;

		// Coins
		int v = 0;
		for (int i = 0; i < c.GetResolution(); i++)
		{
			float range = Random.Range(0f, 1f);
			if (range > coinOffset)
			{
				Vector3 p = c.transform.position + vert[v];
				p.y++;
				GameObject obj = Instantiate (coin, p, Quaternion.identity);
				obj.transform.parent = c.transform;
			}
			v += 2;
		}
		
		// Fuel
		v = 0;
		for (int i = 0; i < c.GetResolution(); i++)
		{
			float range = Random.Range(0f, 1f);
			if (range > fuelOffset)
			{
				Vector3 p = c.transform.position + vert[v];
				p.y++;
				GameObject obj = Instantiate (fuel, p, Quaternion.identity);
				obj.transform.parent = c.transform;
				break;
			}
			v += 2;
		}
		
//		int v = 0;
//		float seedCoins = UnityEngine.Random.Range(0f, 1000);
//		float seedFuel = UnityEngine.Random.Range(0f, 1000);
//		
//		for(int i = 0; i < c.GetResolution(); i++){
//			Vector3 a = vert [v];
//			Vector2 b = vert[v + 1];
//			
//
//			float h = Mathf.PerlinNoise (a.x * c.x + seedCoins, 0f);
//			Debug.Log(h);
//
//			if(h > coinOffset){
//				Vector3 p = c.transform.position + a;
//				p.y++;
//				GameObject obj = Instantiate (coin, p, Quaternion.identity);
//				obj.transform.parent = c.transform;
//			}
//
//			v += 2;
//
//		}
//		v = 0;
//		for(int i = 0; i < c.GetResolution(); i++){
//			Vector3 a = vert [v];
//
//			float h = Mathf.PerlinNoise (a.x * c.x + seedFuel, 0f);
//
//			if(h > fuelOffset){
//				Vector3 p = c.transform.position + a;
//				p.y++;
//				GameObject obj = Instantiate (fuel, p, Quaternion.identity);
//				obj.transform.parent = c.transform;
//				return;
//			}
//
//			v += 2;
//			
//		}
		
	}

}