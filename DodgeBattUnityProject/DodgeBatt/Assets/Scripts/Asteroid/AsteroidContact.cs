using UnityEngine;
using System.Collections;

public class AsteroidContact : MonoBehaviour {

    public GameObject hundredPointsUI;
    public GameObject player;
    public GameObject halfstroid;

    Destroy destroyer;


    // Use this for initialization
    void Start () {
        destroyer = gameObject.GetComponent<Destroy>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDE");
        if (collision.collider.gameObject.tag.Equals("Bat"))
        {
            // Create a vector just in front of the user for a score popup.
            GameObject eyes = GameObject.Find("CenterEyeAnchor");
            Vector3 scorePos = eyes.transform.forward;

            // Instantiate the 100 points prefab at this point.
            GameObject pointsUI = Instantiate(hundredPointsUI, scorePos, Quaternion.identity) as GameObject;
            pointsUI.transform.LookAt(eyes.transform);
            pointsUI.transform.Rotate(new Vector3(0f, 180f, 0f));
            player.GetComponent<PlayerScore>().incrementScore(100);

            // Destroy current prefabs
            Destroy(gameObject.GetComponent<SphereBehaviour>().psystem);
            Destroy(gameObject);

            // Spawn new ones.
            GameObject halfstroid1 = Instantiate(halfstroid, transform.position + new Vector3(0.02f, 0.02f, 0.02f), transform.rotation) as GameObject;
            GameObject halfstroid2 = Instantiate(halfstroid, transform.position + new Vector3(-0.02f, -0.02f, -0.02f), transform.rotation*Quaternion.Euler(180f,0f,0f)) as GameObject;
            halfstroid1.GetComponent<Rigidbody>().AddExplosionForce(15f, transform.position, 2f);
            halfstroid2.GetComponent<Rigidbody>().AddExplosionForce(-15f, transform.position, 2f);
            Destroy(halfstroid1, 5f);
            Destroy(halfstroid2, 5f);


        }

    }
}
