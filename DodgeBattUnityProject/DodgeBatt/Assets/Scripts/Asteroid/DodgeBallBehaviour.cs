using UnityEngine;
using System.Collections;
using System;


public class DodgeBallBehaviour : SphereBehaviour {

	AudioSource fxSound;

	public override void Update () {
		base.Update ();
	}

	public override void OnCollisionEnter(Collision collision) {
		base.OnCollisionEnter (collision);
		GameObject obj = collision.gameObject;
		if (obj.tag == "Player") {
			//Do Something....
			obj.GetComponent<Test>().DecerementHP(5);
			return;
		}
		fxSound = GetComponent<AudioSource> ();
		fxSound.Play ();
	}
}


