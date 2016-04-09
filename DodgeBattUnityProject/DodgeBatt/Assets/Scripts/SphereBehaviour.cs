using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {

	//NOTE: This script assume each sphere is tied to a particle effect.

	//Particle system that's tied to this sphere


	/* Structure for this class and it's children on Update, children will make calls to
	   super.update, when a collision is detected, children make calls to super.onCollisionEnter
	   before doing anything else */

	//This should be set to a new Particle System instance
	//by the manager when a new instance of the sphere 
	//is created
	public ParticleSystem psystem;

	private float delay = 0.0f;

	private bool isOn;

	Rigidbody rb; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		// Initial force vertor can be randomized later
		// This is just here for testing
		Vector3 force = new Vector3 (45.0f, 60.0f, 45.0f);
		rb.AddForce (3.0f * force);

	}
	// Update is called once per frame
	public virtual void Update () {
		delay += Time.deltaTime;
		if (isOn && delay >= 0.5f) {
			isOn = false;
			psystem.Stop ();
		}
	}

	public virtual void OnCollisionEnter(Collision collision) {
		//Check collision against walls
		Vector3 point = collision.contacts [0].point;
		GameObject obj = collision.gameObject;
		if (obj.tag == "Wall1") {
			psystem.GetComponent<Transform> ().position = point;
			//Wall 1 rotation to be handled here
			psystem.GetComponent<Transform> ().rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			psystem.Play ();
			delay = 0.0f;
			isOn = true;
		} else {
			if (obj.tag == "Wall2") {
				psystem.GetComponent<Transform> ().position = point;
				//Wall 2 rotation to be handled here
				psystem.GetComponent<Transform> ().rotation = Quaternion.Euler (0.0f, 0.0f, 90.0f);
				psystem.Play ();
				delay = 0.0f;
				isOn = true;
			} else {
				if (obj.tag == "Wall3") {
					psystem.GetComponent<Transform> ().position = point;
					psystem.GetComponent<Transform> ().rotation = Quaternion.Euler (90.0f, 0.0f, 0.0f);
					//Wall 2 rotation to be handled here
					psystem.Play ();
					delay = 0.0f;
					isOn = true;
				}
			}
		}
	}
}
