using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	/* Note: The GameManager As is will not work over the network! */

	public GameObject ball;

	public float initForce;

	public GameObject partSystem;

    public GameObject p1pos;

	private float spawnTime = 15.0f;

	private float spawnWait = 0.0f;

	private float powerUpSpawnTime = 45.0f;

	private float powerUpWait = 0.0f;

	// Use this for initialization
	void Start () {
        //SpawnBall();
        SpawnBall1Player();
    }

    // Update is called once per frame
    void Update () {
		spawnWait += Time.deltaTime;
		powerUpWait += Time.deltaTime;
		if (spawnWait >= spawnTime) {
			spawnWait = 0.0f;
            //SpawnBall();
            SpawnBall1Player();
        }
	}
		
	private void SpawnBall () {
		GameObject newBall = Instantiate (ball, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		DodgeBallBehaviour newBallBehavior = newBall.GetComponent<DodgeBallBehaviour>();
		GameObject psystem = Instantiate (partSystem, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
        newBallBehavior.initForce = initForce;
        newBallBehavior.psystem = psystem.GetComponent<ParticleSystem>();
	}

    private void SpawnBall1Player()
    {
        // Pick a random spot on the far side of the battle room to spawn a new ball
        Vector3 ballSpawnPaws = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0.5f, 5.5f), 2.5f);
        GameObject newBall = Instantiate(ball, ballSpawnPaws, Quaternion.identity) as GameObject;
        DodgeBallBehaviour newBallBehavior = newBall.GetComponent<DodgeBallBehaviour>();
        GameObject psystem = Instantiate(partSystem, newBall.transform.position, Quaternion.identity) as GameObject;
        newBallBehavior.initForce = initForce;
        newBallBehavior.psystem = psystem.GetComponent<ParticleSystem>();
		newBall.GetComponent<Rigidbody>().AddForce(initForce * (p1pos.transform.position + new Vector3(0f, 0.25f, 0f) - ballSpawnPaws).normalized);
    }
}
