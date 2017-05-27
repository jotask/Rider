using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public GameObject prefab;

	void Start () {
		GameObject obj = Instantiate (prefab);
		obj.transform.localScale = this.transform.localScale;

		Player player = GetComponent<Player>();
		
		Players cfg = player.players[player.player];

		HingeJoint2D hinge = obj.GetComponent<HingeJoint2D> ();
		hinge.connectedBody = GetComponent<Rigidbody2D> ();
		
		hinge.anchor = cfg.headAnchor;
		hinge.connectedAnchor = cfg.connectedAnchorHead;
		
		JointAngleLimits2D limits = new JointAngleLimits2D();
		limits.min = -15;
		limits.max = 15;
		hinge.limits = limits;

		obj.GetComponent<SpriteRenderer>().sprite = cfg.head;

		PolygonCollider2D poly = obj.AddComponent<PolygonCollider2D>();
		poly.isTrigger = true;	
		
		obj.transform.localScale = new Vector3(cfg.scale, cfg.scale, cfg.scale);

	}


}
