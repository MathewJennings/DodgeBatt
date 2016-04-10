using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	/* Note: The GameManager As is will not work over the network! */

	public Transform asteroid;

	public Transform comet;

	public float initForce;

	public Transform partSystem;

	private float spawnTime = 15.0f;

	private float currWait = 0.0f;

	private bool asteroNext = false;

	// Use this for initialization
	void Start () {
		SpawnAstOrComet (asteroid);
	}
	
	// Update is called once per frame
	void Update () {
		currWait += Time.deltaTime;
		if (currWait >= spawnTime) {
			currWait = 0.0f;
			if (asteroNext) {
				SpawnAstOrComet (asteroid);
				asteroNext = false;
			} else {
				SpawnAstOrComet (comet);
				asteroNext = true;
			}
		}
	}
		
	private void SpawnAstOrComet (Transform astOrComet) {
		Object newAst = Instantiate (astOrComet, new Vector3 (0, 0, 0), Quaternion.identity);
		DodgeBallBehaviour newAstBehavior = ((Transform) newAst).gameObject.GetComponent<DodgeBallBehaviour>();
		Object psystem = Instantiate (partSystem, new Vector3 (0, 0, 0), Quaternion.identity);
		newAstBehavior.initForce = initForce;
		newAstBehavior.psystem = ((Transform) psystem).gameObject.GetComponent<ParticleSystem>();
	}
}
