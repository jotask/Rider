using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
	
	readonly Color not = new Color(178f / 255f, 72f / 255f, 72f / 255f);
	readonly Color selected = new Color(80 / 255f, 190 / 255f, 155 / 255f);

	public Toggle[] players;

	void Start()
	{

		if (Utiles.GetBool("boy"))
		{
			players[0].interactable = true;
		}
		else
		{
			Toggle t = players[0];
			t.interactable = false;
			ColorBlock c = t.colors;
			c.disabledColor = not;
			t.colors = c;
		}

		if (Utiles.GetBool("girl"))
		{
			players[1].interactable = true;
		}
		else
		{
			Toggle t = players[1];
			t.interactable = false;
			ColorBlock c = t.colors;
			c.disabledColor = not;
			t.colors = c;
		}

		if (Utiles.GetBool("orc"))
		{
			players[2].interactable = true;
		}
		else
		{
			Toggle t = players[2];
			t.interactable = false;
			ColorBlock c = t.colors;
			c.disabledColor = not;
			t.colors = c;
		}

		int player = PlayerPrefs.GetInt("player", 0);
		selectToggle(players[player]);
		Debug.Log(player);
	}

	public void selectBoy()
	{
		int actual = PlayerPrefs.GetInt("player", 0);
		diselectedToggle(players[actual]);
		selectToggle(players[0]);
		PlayerPrefs.SetInt("player".ToLower(), 0);
		PlayerPrefs.Save();
	}
	public void selectGirl()
	{
		int actual = PlayerPrefs.GetInt("player", 0);
		diselectedToggle(players[actual]);
		selectToggle(players[1]);
		PlayerPrefs.SetInt("player".ToLower(), 1);
		PlayerPrefs.Save();
	}
	public void selectOrc()
	{
		int actual = PlayerPrefs.GetInt("player", 0);
		diselectedToggle(players[actual]);
		selectToggle(players[2]);
		PlayerPrefs.SetInt("player".ToLower(), 2);
		PlayerPrefs.Save();
	}

	public void Play()
	{
		Loading.LoadScene(Loading.Scenes.GAME);
	}

	private void diselectedToggle(Toggle t)
	{
		ColorBlock c = t.colors;
		c.normalColor = Color.white;
		c.highlightedColor = Color.white;
		t.colors = c;
	}

	private void selectToggle(Toggle t)
	{
		ColorBlock c = t.colors;
		c.normalColor = selected;
		c.highlightedColor = selected;
		t.colors = c;
	}

	void Update()
	{
		Debug.Log(PlayerPrefs.GetInt("player", 0));
	}
	
}
