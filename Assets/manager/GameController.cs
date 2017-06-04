using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int score { get; private set; }

	private int current;

	public Text total;
	public Text text;

	void Start ()
	{
		this.score = 0;
		current = PlayerPrefs.GetInt("Money");
		total.text = "Total: " + current;
	}

	public void AddScore(int value)
	{
		this.score += value;
		this.text.text = "Score: " + this.score;
		this.total.text = "Total: " + (current + score);
	}

	public void Save()
	{
		int money = PlayerPrefs.GetInt("Money", 0);
		PlayerPrefs.SetInt("Money", money + score);
		PlayerPrefs.Save();
	}

}
