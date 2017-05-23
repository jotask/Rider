using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public GameObject prefab;

	void Awake () {
		GameObject obj = Instantiate (prefab);
		obj.transform.localScale = this.transform.localScale;

		HingeJoint2D hinge = obj.GetComponent<HingeJoint2D> ();
		hinge.connectedBody = GetComponent<Rigidbody2D> ();
		hinge.anchor = new Vector2 (-0f, -2f);
		hinge.connectedAnchor = new Vector2 (-0.9f, 1f);
		JointAngleLimits2D limits = new JointAngleLimits2D();
		limits.min = -15;
		limits.max = 15;
		hinge.limits = limits;

	}


}
