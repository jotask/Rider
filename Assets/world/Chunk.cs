using System;
using UnityEngine;

[RequireComponent (typeof (MeshFilter), typeof (MeshRenderer))]
[RequireComponent (typeof (EdgeCollider2D))]
public class Chunk : MonoBehaviour
{

//	const float depth = 6f;
//	const float heightOffset = 10f;
//
//	const float amplitude = 7f;

	public const float SIZE = 10f;

	private Mesh mesh;

	public Vector3[] vertices { get; private set; }
	private Vector2[] uvs;
	private int[] triangles;

	private EdgeCollider2D edge;

	public int x { get; private set; }

	void Awake () {
		this.edge = GetComponent<EdgeCollider2D> ();
	}

	public void reset(int x, float[] noise, float amplitude){
		this.x = x;
		this.transform.position = new Vector3 (x * SIZE, 0, 0);
		this.name = "Chunk: " + this.x;
		//setColl(noise);
		create(noise, amplitude);
	}

	private void create(float[] noise, float amplitude){
		
		int resolution = noise.Length - 1;

		float incr = SIZE / (resolution);

		this.vertices = new Vector3[(resolution + 1) * 3];
		this.uvs = new Vector2[(resolution + 1) * 3];
		this.triangles = new int[((resolution) * 12)];

		int v = 0;
		for(int i = 0; i <= resolution; i++){
			
			float h = noise[i];

			Vector3 a = new Vector3 (i * incr,  h * amplitude, 6f);
			Vector3 b = new Vector3 (i * incr,  h * amplitude, 0);
			Vector3 c = new Vector3 (i * incr, -10f, 0);

			vertices [v + 0] = a;
			vertices [v + 1] = b;
			vertices [v + 2] = c;

			this.uvs[v + 0] = new Vector2(i / (float) resolution, 0f);
			this.uvs[v + 1] = new Vector2(i / (float) resolution, .05f);
			this.uvs[v + 2] = new Vector2(i / (float) resolution, 1f);

			v += 3;

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
        
		Vector2[] pos = new Vector2[noise.Length];
		for (int i = 0; i < pos.Length; i++)
		{
			pos[i] = new Vector2(i * incr, noise[i] * amplitude);
		}
		this.edge.points = pos;

		Mesh mesh = new Mesh();
		mesh.vertices  = this.vertices;
		mesh.uv = this.uvs;
		mesh.triangles = this.triangles;

		mesh.normals = calculateNormals ();

		GetComponent<MeshFilter> ().mesh = mesh;

		GetComponent<Renderer> ().material.mainTexture = createTexture ();

	}

	private Texture2D createTexture(){
		int widht = (int) SIZE;
		int height = Mathf.Abs(10);

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

	private Vector3[] calculateNormals(){
		Vector3[] normals = new Vector3[vertices.Length];
		int trianglesCount = triangles.Length / 3;
		for(int i = 0; i < trianglesCount; i++){
			int normalTriangleIndex = i * 3;
			int vertexIndexA = triangles [normalTriangleIndex + 0];
			int vertexIndexB = triangles [normalTriangleIndex + 1];
			int vertexIndexC = triangles [normalTriangleIndex + 2];

			Vector3 triangleNormal = surfaceNormalFromIndices (vertexIndexA, vertexIndexB, vertexIndexC);
			normals [vertexIndexA] += triangleNormal;
			normals [vertexIndexB] += triangleNormal;
			normals [vertexIndexC] += triangleNormal;
			
		}

		for(int i = 0; i < normals.Length; i++){
			normals [i].Normalize ();
		}

		return normals;

	}

	private Vector3 surfaceNormalFromIndices(int a, int b, int c){
		Vector3 pointA = vertices [a];
		Vector3 pointB = vertices [b];
		Vector3 pointC = vertices [c];

		Vector3 sideAB = pointB - pointA;
		Vector3 sideAC = pointC - pointA;

		return Vector3.Cross (sideAB, sideAC).normalized;

	}

	public void actualizar(float[] noise, float amplitude)
	{
		create(noise, amplitude);
	}

	public int GetResolution()
	{
		return this.vertices.Length / 3;
	}

}
