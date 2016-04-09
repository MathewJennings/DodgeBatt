using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {

	//NOTE: This script assume each sphere is tied to a particle effect.

	//Particle system that's tied to this sphere
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
	void Update () {
		delay += Time.deltaTime;
		if (isOn && delay >= 0.5f) {
			isOn = false;
			psystem.Stop ();
			print ("Stopped");
		}
	}

	void OnCollisionEnter(Collision collision) {
//		foreach (ContactPoint contact in collision.contacts) {
//			print ("" + colNum + ": " + contact.point);
//		}
//		print("" + colNum + ": " + GetComponent<Transform>().position);
//		colNum = colNum + 1;
		Vector3 point = collision.contacts[0].point;
		psystem.GetComponent<Transform> ().position = point;
		psystem.Play ();
		delay = 0.0f;
		isOn = true;
	}
}
