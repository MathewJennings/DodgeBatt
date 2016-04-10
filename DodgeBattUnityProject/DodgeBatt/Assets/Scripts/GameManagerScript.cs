
using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	/* Note: The GameManager As is will not work over the network! */

	public GameObject asteroid;

	public float initForce;

	public GameObject partSystem;

    public GameObject p1pos;

	private GameObject player = null;
	private float spawnTime = 5.0f;

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
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("player");
		}
		spawnWait += Time.deltaTime;
		powerUpWait += Time.deltaTime;
		if (spawnWait >= spawnTime) {
			spawnWait = 0.0f;
            SpawnBall1Player();
        }
		if (player.GetComponent<Test> ().GetHP () <= 0) {
			//Enable Game Over sequence!!
		}
	}

    private void SpawnBall1Player()
    {
        // Pick a random spot on the far side of the battle room to spawn a new ball
        Vector3 spawnPos = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f));
        GameObject newAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity) as GameObject;
        DodgeBallBehaviour newBallBehavior = newAsteroid.GetComponent<DodgeBallBehaviour>();
        GameObject psystem = Instantiate(partSystem, newAsteroid.transform.position, Quaternion.identity) as GameObject;
        newBallBehavior.initForce = initForce;
        newBallBehavior.psystem = psystem.GetComponent<ParticleSystem>();
        newAsteroid.GetComponent<Rigidbody>().AddForce(initForce * (p1pos.transform.position + new Vector3(0f, 0.25f, 0f) - spawnPos).normalized);
    }
}
