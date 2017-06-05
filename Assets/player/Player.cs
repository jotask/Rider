using UnityEngine;

public class Player : MonoBehaviour
{

	public Players[] players;

	public GameObject frontWheel;
	public GameObject backWheel;

	[HideInInspector]
	public int player { get; private set; }

	public bool desktop;
	public bool invecible;
	public bool infiniteFuel;
	
	private GameController gameController;

	private Motor motor;

	void Awake()
	{
		
		gameObject.transform.localScale = Vector3.one;
		
		player = PlayerPrefs.GetInt("player", 0);
		
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		Players cfg = players[player];
		
		GetComponent<SpriteRenderer>().sprite = cfg.carBody;
		gameObject.AddComponent<PolygonCollider2D>();
		
		setWheel(cfg, frontWheel);
		setWheel(cfg, backWheel);

		gameObject.transform.localScale = new Vector3(cfg.scale, cfg.scale, cfg.scale);
		
		motor = GetComponent<Motor>();
		
		CreateHead();
		
//		Tricks tricks = gameObject.AddComponent<Tricks>();
//		
//		tricks.front = frontWheel.GetComponent<Wheel>();
//		tricks.back  = backWheel.GetComponent<Wheel>();

	}

	private void setWheel(Players cfg, GameObject obj)
	{
		obj.transform.localScale = Vector3.one;
		obj.GetComponent<CircleCollider2D>().radius = cfg.wheelRadious;
		obj.GetComponent<SpriteRenderer>().sprite = cfg.wheel;
		obj.GetComponent<SpriteRenderer>().sortingOrder = cfg.wheelRendererLayer;
		obj.transform.localScale = Vector3.one * cfg.wheelScale;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;
		if(tag == "Coin"){
			AudioManager.instance.PlaySound2D("coin");
			int value = other.GetComponent<Coin>().getValue();
			gameController.AddScore(value);
			Destroy (other.gameObject);
		}else if (tag == "Fuel")
		{
			motor.fuel += 1f;
			Destroy(other.gameObject);
		}
	}

	private void CreateHead()
	{
		
		Players cfg = players[player];
		
		GameObject obj = new GameObject("PlayerHead");
		obj.layer = LayerMask.NameToLayer("Car");
		obj.transform.position = this.transform.position;
		
		
		Rigidbody2D body = obj.AddComponent<Rigidbody2D>();
		body.mass = 0.0001f;
		body.bodyType = RigidbodyType2D.Dynamic;
		
		HingeJoint2D joint = obj.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		
		SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
		sr.sprite = cfg.head;

		joint.connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		
		joint.anchor = cfg.headAnchor;
		joint.connectedAnchor = cfg.connectedAnchorHead;
		
		JointAngleLimits2D limits = new JointAngleLimits2D();
		limits.min = -15;
		limits.max = 15;
		joint.limits = limits;

		PolygonCollider2D poly = obj.AddComponent<PolygonCollider2D>();
		poly.isTrigger = true;	
		
		obj.transform.localScale = new Vector3(cfg.scale, cfg.scale, cfg.scale);
		
		obj.AddComponent<HeadCollider>();
		
	}

}
