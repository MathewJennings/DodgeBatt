using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	/* Note: The GameManager As is will not work over the network! */

	public Transform ball;

	public float initForce;

	public Transform partSystem;

	private float spawnTime = 15.0f;

	private float currWait = 0.0f;

	// Use this for initialization
	void Start () {
        SpawnBall();
	}
	
	// Update is called once per frame
	void Update () {
		currWait += Time.deltaTime;
		if (currWait >= spawnTime) {
			currWait = 0.0f;
            SpawnBall();
        }
	}
		
	private void SpawnBall () {
		Object newBall = Instantiate (ball, new Vector3 (0, 0, 0), Quaternion.identity);
		DodgeBallBehaviour newBallBehavior = ((Transform)newBall).gameObject.GetComponent<DodgeBallBehaviour>();
		Object psystem = Instantiate (partSystem, new Vector3 (0, 0, 0), Quaternion.identity);
        newBallBehavior.initForce = initForce;
        newBallBehavior.psystem = ((Transform) psystem).gameObject.GetComponent<ParticleSystem>();
	}
}
