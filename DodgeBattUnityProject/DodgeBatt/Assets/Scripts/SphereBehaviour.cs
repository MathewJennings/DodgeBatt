using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {

	//NOTE: This script assume each sphere is tied to a particle effect.

	//Particle system that's tied to this sphere


	/* Structure for this class and it's children on Update, children will make calls to
	   super.update, when a collision is detected, children make calls to super.onCollisionEnter
	   before doing anything else */

	public ParticleSystem psystem;

	private float delay = 0.0f;

	private bool isOn;

	Rigidbody rb; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		// Initial force vertor can be randomized later
		// This is just here for testing
		Vector3 force = new Vector3 (Mathf.Cos (Mathf.PI / 6.0f), 0.0f, Mathf.Sin (Mathf.PI / 6.0f));
		rb.AddForce (150.0f * force);

	}
	// Update is called once per frame
	public virtual void Update () {
		delay += Time.deltaTime;
		if (isOn && delay >= 0.5f) {
			isOn = false;
			psystem.Stop ();
			print ("Stopped");
		}
	}

	public virtual void OnCollisionEnter(Collision collision) {
//		foreach (ContactPoint contact in collision.contacts) {
//			print ("" + colNum + ": " + contact.point);
//		}
//		print("" + colNum + ": " + GetComponent<Transform>().position);
//		colNum = colNum + 1;
		Vector3 point = collision.contacts[0].point;
		GameObject obj = collision.gameObject;
		if (obj.tag == "wall1") {
			psystem.GetComponent<Transform> ().position = point;
			//Wall 1 rotation to be handled here
			psystem.Play ();
			delay = 0.0f;
			isOn = true;
		} else {
			if (obj.tag == "wall2") {
				psystem.GetComponent<Transform> ().position = point;
				//Wall 2 rotation to be handled here
				psystem.Play ();
				delay = 0.0f;
				isOn = true;
			}
		}
	}
}
