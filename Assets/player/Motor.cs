using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour {

	//public float suspension;

	public float speed = 1500;
	public float movement = 1;

	public WheelJoint2D back;
	public WheelJoint2D front;

	void Awake(){
		
	}

	void Update () {
		this.movement = -Input.GetAxisRaw ("Horizontal") * speed;
	}

	void FixedUpdate(){

		if (movement == 0) {
			back.useMotor = false;
			front.useMotor = false;
		} else {
			back.useMotor = true;
			front.useMotor = true;

			JointMotor2D motor = new JointMotor2D{
				motorSpeed = movement,
				maxMotorTorque = back.motor.maxMotorTorque
			};

			back.motor = motor;
			front.motor = motor;
		}

	}

}
