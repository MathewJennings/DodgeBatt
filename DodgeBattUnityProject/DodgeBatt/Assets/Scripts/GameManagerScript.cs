using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	/* Note: The GameManager As is will not work over the network! */

	public GameObject ball;

	public float initForce;

	public GameObject partSystem;

    public GameObject p1pos;

	private float spawnTime = 15.0f;

	private float currWait = 0.0f;

	// Use this for initialization
	void Start () {
        //SpawnBall();
        SpawnBall1Player();
    }

    // Update is called once per frame
    void Update () {
		currWait += Time.deltaTime;
		if (currWait >= spawnTime) {
			currWait = 0.0f;
            //SpawnBall();
            SpawnBall1Player();
        }
	}
		
	private void SpawnBall () {
		Object newBall = Instantiate (ball, new Vector3 (0, 0, 0), Quaternion.identity);
		DodgeBallBehaviour newBallBehavior = ((Transform)newBall).gameObject.GetComponent<DodgeBallBehaviour>();
		Object psystem = Instantiate (partSystem, new Vector3 (0, 0, 0), Quaternion.identity);
        newBallBehavior.initForce = initForce;
        newBallBehavior.psystem = ((Transform) psystem).gameObject.GetComponent<ParticleSystem>();
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
        Debug.Log("force: " + initForce * (p1pos.transform.position - ballSpawnPaws));
        newBall.GetComponent<Rigidbody>().AddForce(initForce * (p1pos.transform.position - ballSpawnPaws));
    }
}
