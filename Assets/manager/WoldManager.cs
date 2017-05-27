using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

	private ItemManager itemManager;

	void Awake()
	{
		Time.timeScale = 1f;
	}

	void Start(){

		AudioManager.instance.PlayMusic(MusicLibrary.Scene.PLAY);
		
		this.itemManager = GetComponent<ItemManager> ();
		this.player = GameObject.FindGameObjectWithTag ("Player").transform;

		this.chunks = new List<Chunk> ();

		int initial = 0;
	
		System.Random r = new System.Random();
		this.seed = r.Next();

		this.left = initial;
		this.right = initial;

		for (int i = 0; i < size; i++)
		{
			Chunk obj = Instantiate(prefab) as Chunk;
			obj.transform.parent = this.transform;
			obj.transform.position = new Vector3(i * Chunk.SIZE, 0, 0);
			if (i < 2)
			{
				emptyChunk(obj, this.right++);
			} else {
				this.reset(obj, this.right++);
			}
			this.chunks.Add (obj);
		}
		
		ChunkTransation(1);

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
		this.itemManager.newChunk (c);
	}

	private void emptyChunk(Chunk c, int x)
	{
		float[] noise = new float[resolution + 1];
		for (int i = 0; i < noise.Length; i++)
		{
			noise[i] = 0;
		}
		c.reset(x, noise, amplitude);
	}

	private void ChunkTransation(int i)
	{
		Chunk c = this.chunks[i];
		Chunk pre  = this.chunks[i - 1];
		Chunk post = this.chunks[i + 1];
		float presX = pre.vertices[pre.vertices.Length - 2].y;
		float postX = post.vertices[0].y + .14f;
		float[] noise = new float[resolution + 1];
		for (int j = 0; j < noise.Length; j++)
		{
			float tmp = Remap(j, 0, noise.Length, presX, postX);
			noise[j] = tmp;
		}
		c.reset(c.x, noise, 1f);
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
	
	public static float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	
}