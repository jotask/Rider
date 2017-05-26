using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoldManager : MonoBehaviour {
	
	public int resolution = 10;
	
	public float noiseScale;

	public float amplitude;

	[Range(0, 8)]
	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public float offset;

	public bool autoUpdate;
	[Range(0,1)]
	public float velocity;

	const int size = 4;

	private Transform player;

	List<Chunk> chunks;

	public Chunk prefab;

	private int left;
	private int right;

	private CoinManager coins;

	void Awake()
	{
		Time.timeScale = 1f;
	}

	void Start(){

		AudioManager.instance.PlayMusic(MusicLibrary.Scene.PLAY);
		
		this.coins = GetComponent<CoinManager> ();
		this.player = GameObject.FindGameObjectWithTag ("Player").transform;

		this.chunks = new List<Chunk> ();

		int initial = 0;
	
		System.Random r = new System.Random();
		this.seed = r.Next();

		this.left = initial;
		this.right = initial;

		for(int i = 0; i < size; i++){
			Chunk obj = Instantiate (prefab) as Chunk;
			obj.transform.parent = this.transform;
			obj.transform.position = new Vector3 ( i * Chunk.SIZE, 0, 0);
			this.reset (obj, this.right++);
			this.chunks.Add (obj);
		}

	}

	void Update(){
		
		foreach(Chunk c in this.chunks){
			float dst = calculate (c);
			if(dst > Chunk.SIZE * (size / 2)){
				this.reset (c, this.right);
				this.right++;
				this.left++;
			}else if(dst < -Chunk.SIZE * (size / 2)){
				this.left--;
				this.right--;
				this.reset (c, this.left);
			}
		}

		if (autoUpdate)
		{
			for (int i = 0; i < chunks.Count; i++)
			{
				Chunk c = chunks[i].GetComponent<Chunk>();
				float x = c.x * resolution;
				float[] noise = GenerateMapData(x);
				c.reset(i, noise, amplitude);
			}

			offset += velocity;
			
		}

	}

	private void reset(Chunk c, int x){
		float[] noise = GenerateMapData(x * resolution);
		c.reset (x, noise, amplitude);
		this.coins.newChunk (c);
	}

	private float calculate(Chunk c){
		Vector3 a = player.transform.position;
		Vector3 b = c.transform.position;
		return (a.x - b.x);
	}

	public float[] GenerateMapData(float x) {
		float[] noiseMap = NoiseGenerator.GenerateNoise (resolution + 1, seed, noiseScale, octaves, persistance, lacunarity, x + offset);
		return noiseMap;
	}

	void OnValidate() {
		if (lacunarity < 1) 
			lacunarity = 1;
		
		if (octaves < 1) 
			octaves = 1;
		
		if (noiseScale < 1f)
			noiseScale = 1f;
		
		if (resolution < 1)
			resolution = 1;
		
		if (amplitude < 1f)
			amplitude = 1f;

	}
	
}