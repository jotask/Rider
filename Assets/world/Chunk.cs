using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MeshFilter), typeof (MeshRenderer), typeof(MeshCollider))]
public class Chunk : MonoBehaviour {


	const float seed = 0f;
	const float depth = 6f;
	const float heightOffset = 10f;

	const float amplitude = 7f;

	public const int size = 10;

	const int resolution = 20;

	private Mesh mesh;

	public Vector3[] vertices { get; private set; }
	private Vector2[] uvs;
	private int[] triangles;

	private int x;

	void Awake () {
		reset (0);
	}

	public void reset(int x){
		this.x = x;
		this.transform.position = new Vector3 (x* size, 0, 0);
		this.name = "Chunk: " + this.x;
		cub ();
	}

	private void cub(){

		float incr = size / (float) resolution;

		this.vertices = new Vector3[(resolution + 1) * 3];
		this.uvs = new Vector2[(resolution + 1) * 3];
		this.triangles = new int[((resolution) * 12)];

		int v = 0;
		for(int i = 0; i <= resolution; i++){
			float h = Mathf.PerlinNoise (x + (i * incr / size) + seed, (.5f) + seed);

			Vector3 a = new Vector3 (i * incr,  h * amplitude , depth);
			Vector3 b = new Vector3 (i * incr,  h * amplitude , 0);
			Vector3 c = new Vector3 (i * incr, -heightOffset, 0);

			vertices [v + 0] = a;
			vertices [v + 1] = b;
			vertices [v + 2] = c;

			this.uvs[v + 0] = new Vector2(i / (float) resolution, 0f);
			this.uvs[v + 1] = new Vector2(i / (float) resolution, .05f);
			this.uvs[v + 2] = new Vector2(i / (float) resolution, 1f);

			v += 3;

			//ins (a);
			//ins (b);
			//ins (c);

		}

		v = 0;
		int t = 0;
		for (int i = 0; i < resolution; i++) {
			this.triangles [t++] = v + 0;
			this.triangles [t++] = v + 4;
			this.triangles [t++] = v + 1;

			this.triangles [t++] = v;
			this.triangles [t++] = v + 3;
			this.triangles [t++] = v + 4;

			this.triangles [t++] = v + 1;
			this.triangles [t++] = v + 5;
			this.triangles [t++] = v + 2;

			this.triangles [t++] = v + 1;
			this.triangles [t++] = v + 4;
			this.triangles [t++] = v + 5;


			v += 3;
		}

		Mesh mesh = new Mesh();
		mesh.vertices  = this.vertices;
		mesh.uv = this.uvs;
		mesh.triangles = this.triangles;

		mesh.RecalculateNormals ();

		GetComponent<MeshFilter> ().mesh = mesh;

		GetComponent<Renderer> ().material.mainTexture = createTexture ();

		GetComponent<MeshCollider> ().sharedMesh = mesh;

	}

	private Texture2D createTexture(){
		int widht = size;
		int height = Mathf.Abs((int) amplitude + (int) depth + (int) heightOffset);

		Color[] colors = new Color[widht * height];
		for(int y = 0; y < height; y++){
			for(int x = 0; x < widht; x++){
				Color color = Color.magenta;
				if(y < 1){
					color = Color.blue;
				}else if(y < 2f){
					color = Color.green;
				}
				//colors [y * widht + x] = Color.Lerp (Color.green, Color.black
				//, Mathf.PerlinNoise(this.transform.position.x + x / (float)widht, this.transform.position.y + y / (float)height));
				colors[y * widht + x] = color;
			}
		}

		Texture2D texture = new Texture2D (widht, height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels (colors);
		texture.Apply ();
		return texture;
	}

	private GameObject ins(Vector3 p){
		GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		obj.transform.position = p;
		obj.transform.parent = this.transform;
		return obj;
	}

}
