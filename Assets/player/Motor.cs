using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody2D))]
public class Motor : MonoBehaviour {

	public float speed = 1500;
	public float rotationSpeed = 15f;

	private float movement = 0f;
	private float rotation = 0f;

	public WheelJoint2D back;
	public WheelJoint2D front;

	private Rigidbody2D body;

	private Player _player;

	public Slider fuelSlider;

	public float fuel = 1f;

	public bool autoMotor;

	public float currentSpeed = 0f;

	void Awake(){
		this.body = GetComponent<Rigidbody2D> ();
		this._player = GetComponent<Player>();

		back.anchor = _player.players[_player.player].backAnchor;
		front.anchor = _player.players[_player.player].frontAnchor;

		this.fuelSlider.value = fuel;

	}

	void Update () {
		if (autoMotor){
			if (currentSpeed < speed)
			{
				currentSpeed += Time.deltaTime * 750f;
			}
			this.movement = -1f * currentSpeed;
		} else {
			this.movement = -Input.GetAxisRaw ("Vertical") * this.speed;
		}

		this.fuel -= 0.05f * Time.deltaTime;
		this.fuelSlider.value = this.fuel;
		if (this.fuel < .10f)
		{
			this.fuelSlider.fillRect.GetComponent<Image>().color = Color.red;
		}
		else if (this.fuel < .5f)
		{
			this.fuelSlider.fillRect.GetComponent<Image>().color = Color.yellow;
		}else
		{
			this.fuelSlider.fillRect.GetComponent<Image>().color = Color.green;
		}

	}

	void FixedUpdate(){

		if (movement == 0) {
			this.back.useMotor = false;
			this.front.useMotor = false;
		} else {
			this.back.useMotor = true;
			this.front.useMotor = true;

			JointMotor2D motor = new JointMotor2D{
				motorSpeed = this.movement,
				maxMotorTorque = this.back.motor.maxMotorTorque
			};

			this.back.motor = motor;
			this.front.motor = motor;
		}

		if(_player.desktop)
			this.rotation = Input.GetAxisRaw("Horizontal") * rotationSpeed;
		
		this.body.AddTorque (-rotation * Time.fixedDeltaTime);

	}

	public void LeftRotation()
	{
		this.rotation = -1f * this.rotationSpeed;
	}

	public void RightRotation()
	{
		this.rotation = 1f * this.rotationSpeed;
	}

	public void StopRotation()
	{
		this.rotation = 0f;
	}

}
