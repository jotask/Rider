using UnityEngine;

public class Empty : MonoBehaviour
{

	private const int version = 1;

	void Awake ()
	{

		int ver = PlayerPrefs.GetInt("version", -1);
		
		if (ver != ver)
		{
			PlayerPrefs.DeleteAll();
			Utiles.SetBool("boy", true);
		}
			
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}
