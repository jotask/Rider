using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	public Text money;

	public Button coin;
	public Button fuel;

	void Start()
	{

		money.text = "Money: " + PlayerPrefs.GetInt("Money", 0);
		
		Text[] tcoin = coin.GetComponentsInChildren<Text>();
		tcoin[1].text = PlayerPrefs.GetFloat(ItemManager.Items.COINS.ToString().ToLower(), 1f) + " %";
		
		Text[] tfuel = fuel.GetComponentsInChildren<Text>();
		tfuel[1].text = PlayerPrefs.GetFloat(ItemManager.Items.FUEL.ToString().ToLower(), 1f) + " %";
		
	}

	public void UpgradeCoin()
	{
		Debug.Log("upgrade Coin");
	}

	public void UpgradeFuel()
	{
		Debug.Log("upgrade Fuel");
	}

	public void BackToMenu()
	{
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}