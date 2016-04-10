using UnityEngine;
using System.Collections;
using System;


public class DodgeBallBehaviour : SphereBehaviour {
	
	public override void Update () {
		base.Update ();
	}

	public override void OnCollisionEnter(Collision collision) {
		base.OnCollisionEnter (collision);
		GameObject obj = collision.gameObject;
		if (obj.tag == "bat") {
			//Do Something....
			Vector3 vel = obj.GetComponent<Rigidbody>().velocity;
		}
	}
}


