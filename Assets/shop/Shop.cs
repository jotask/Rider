using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	public Players[] players;
	
	public Text money;

	public Button coin;
	public Button fuel;

	public Button boy;
	public Button girl;
	public Button orc;

	private int coinCostBasic = 100;
	private int fuelCostBasic = 100;

	void Start()
	{

		int dinero = GetDinero();

		money.text = "Money: " + dinero;

		Text[] tcoin = coin.GetComponentsInChildren<Text>();
		int coins = PlayerPrefs.GetInt(ItemManager.Items.COINS.ToString().ToLower(), 0);
		tcoin[1].text = "Level: " + coins;
		tcoin[2].text = "Upgrade : " + calculateCost(coins, fuelCostBasic) + " $";

		Text[] tfuel = fuel.GetComponentsInChildren<Text>();
		int fuels = PlayerPrefs.GetInt(ItemManager.Items.FUEL.ToString().ToLower(), 0);
		tfuel[1].text = "Level: " + fuels;
		tfuel[2].text = "Upgrade : " + calculateCost(fuels, fuelCostBasic) + " $";

		Color not = new Color(178f / 255f, 72f / 255f, 72f / 255f);
		Color able = new Color(150f / 255f, 240f / 255f, 165f / 255f);

		if (Utiles.GetBool("boy", true))
		{
			boy.interactable = false;
		}

		if (Utiles.GetBool("girl", false))
		{
			girl.interactable = false;
			Text t = girl.GetComponentsInChildren<Text>()[1];
			t.text = "Owned";
		}
		else
		{
			if (dinero < 1000)
			{
				girl.interactable = false;
				ColorBlock c = girl.colors;
				c.disabledColor = not;
				girl.colors = c;
			}
			else
			{
				girl.interactable = true;
				ColorBlock c = girl.colors;
				c.normalColor = able;
				girl.colors = c;
			}
		}
		
		if (Utiles.GetBool("orc", false))
		{
			orc.interactable = false;
			Text t = orc.GetComponentsInChildren<Text>()[1];
			t.text = "Owned";
		}
		else
		{
			if (dinero < 10000)
			{
				orc.interactable = false;
				ColorBlock c = orc.colors;
				c.disabledColor = not;
				orc.colors = c;
			}
			else
			{
				orc.interactable = true;
				ColorBlock c = orc.colors;
				c.normalColor = able;
				orc.colors = c;
			}
		}
	}

	private int GetDinero()
	{
		return PlayerPrefs.GetInt(Utiles.Prefs.MONEY.ToString().ToLower());
	}

	public void UpgradeCoin()
	{
		int dinero = GetDinero();
		string name = ItemManager.Items.COINS.ToString().ToLower();
		int level = PlayerPrefs.GetInt(name, 0);
		int cost = calculateCost(level, coinCostBasic);
		if (dinero < cost)
		{
			return;
		}
		AddMoney(-cost);
		PlayerPrefs.SetInt(name, ++level);
		Text[] tcoin = coin.GetComponentsInChildren<Text>();
		tcoin[1].text = "Level: " + level;
		tcoin[2].text = "Upgrade : " + calculateCost(level, coinCostBasic) + " $";

	}

	public void UpgradeFuel()
	{
		int dinero = GetDinero();
		string name = ItemManager.Items.FUEL.ToString().ToLower();
		int level = PlayerPrefs.GetInt(name, 0);
		int cost = calculateCost(level, fuelCostBasic);
		if (dinero < cost)
		{
			return;
		}
		AddMoney(-cost);
		PlayerPrefs.SetInt(name, ++level);
		Text[] tfuel = fuel.GetComponentsInChildren<Text>();
		tfuel[1].text = "Level: " + level;
		tfuel[2].text = "Upgrade : " + calculateCost(level, fuelCostBasic) + " $";
	}

	private int calculateCost(int value, int basic)
	{
		return Mathf.RoundToInt(value * basic * 2f);
	}

	public void BackToMenu()
	{
		Loading.LoadScene(Loading.Scenes.MENU);
	}

	public void restart()
	{
		PlayerPrefs.SetInt(ItemManager.Items.FUEL.ToString().ToLower(), 0);
		PlayerPrefs.SetInt(ItemManager.Items.COINS.ToString().ToLower(), 0);
		
		Utiles.SetBool("boy", true);
		Utiles.SetBool("girl", false);
		Utiles.SetBool("orc", false);
		
		PlayerPrefs.SetInt("money", 1000);
		
		Start();
		
	}

	public void BuyGirl()
	{
		Utiles.SetBool("girl", true);
		AddMoney(-1000);
		Start();
	}

	public void BuyOrc()
	{
		Utiles.SetBool("orc", true);
		AddMoney(-10000);
		Start();
	}

	public void AddMoney(int price)
	{
		int money = GetDinero();
		money += price;
		if (money < 0)
		{
			money = 0;
		}
		PlayerPrefs.SetInt(Utiles.Prefs.MONEY.ToString().ToLower(), money);
		Start();
	}

}