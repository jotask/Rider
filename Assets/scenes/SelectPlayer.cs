using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour {

	public void selectBoy()
	{
		PlayerPrefs.SetInt("player".ToLower(), 0);
		PlayerPrefs.Save();
	}
	public void selectGirl()
	{
		PlayerPrefs.SetInt("player".ToLower(), 1);
		PlayerPrefs.Save();
	}
	public void selectOrc()
	{
		PlayerPrefs.SetInt("player".ToLower(), 2);
		PlayerPrefs.Save();
	}

	public void Play()
	{
		Loading.LoadScene(Loading.Scenes.GAME);
	}

}
