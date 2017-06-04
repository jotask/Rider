using UnityEngine;

public class Empty : MonoBehaviour
{

	private const int version = 1;

	void Awake ()
	{

		int ver = PlayerPrefs.GetInt("version", -1);
		
		if (ver != version)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("version", version);
			Utiles.SetBool("boy", true);
		}
			
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}
