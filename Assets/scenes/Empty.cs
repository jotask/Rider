using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Empty : MonoBehaviour
{

	void Awake ()
	{

		string name = Application.version;
		string res = Regex.Replace(name.ToLower(), @"[^a-z0-9_]+", String.Empty);
		int version = int.Parse(res);

		int ver = PlayerPrefs.GetInt("version", 0);
	
		if (ver != version)
		
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("version", version);
			Utiles.SetBool("boy", true);
		}
			
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}
