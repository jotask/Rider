using UnityEngine;

public class HeadCollider : MonoBehaviour
{

	private Hud hud;

	private Player player;

	void Start ()
	{
		hud = GameObject.FindGameObjectWithTag("Ui").GetComponent<Hud>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.ToLower() == "world" && !player.invecible)
		{
			hud.GameOver();
		}
	}
	
}
