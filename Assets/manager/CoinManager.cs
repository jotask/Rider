using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {
	
	[Range(0f, 1f)]
	public float offset = .9f;

	public GameObject prefab;

	public void newChunk(Chunk c){
		Vector3[] vert = c.vertices;
		int v = 0;
		for(int i = 0; i < c.GetResolution(); i++){
			Vector3 a = vert [v + 1];

			float h = Mathf.PerlinNoise (a.x, a.y);

			if(h > offset){
				Vector3 p = c.transform.position + a;
				p.y++;
				GameObject obj = Instantiate (prefab, p, Quaternion.identity);
				obj.transform.parent = c.transform;
			}

			v += 3;
			
		}
		
	}

}