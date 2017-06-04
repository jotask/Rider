using UnityEngine;

public class ItemManager : MonoBehaviour {

	public enum Items
	{
		COINS, FUEL
	}

	private const float REDUCE = 5f;

	private float coinOffset = 1f;

	private float fuelOffset = 1f;

	public GameObject coin;
	public GameObject fuel;

	void Awake()
	{
		
		int c = PlayerPrefs.GetInt(Items.COINS.ToString().ToLower(), 0);
		int f = PlayerPrefs.GetInt(Items.FUEL.ToString().ToLower(), 0);

		coinOffset = GetReducer(c);
		fuelOffset = GetReducer(f);
		
	}

	private float GetReducer(int level)
	{
		int multuplier = 0;
		for (int i = 0; i < level; i++) {
			if ( i % 10 == 0)
			{
				multuplier++;
			}
		}
		float reducer = (REDUCE * multuplier) / 100f;
		return 1f - reducer;
	}

	public void newChunk(Chunk c){
		Vector3[] vert = c.vertices;

		// Coins
		int v = 0;
		for (int i = 0; i < c.GetResolution(); i+=2)
		{
			if (i % 5 != 0)
			{
				continue;
			}
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
				obj.transform.localScale = new Vector3(.5f, .5f, .5f);
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