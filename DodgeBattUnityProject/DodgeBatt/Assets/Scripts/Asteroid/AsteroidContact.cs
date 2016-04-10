using UnityEngine;
using System.Collections;

public class AsteroidContact : MonoBehaviour {

    public GameObject hundredPointsUI;
    public GameObject player;

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

            Invoke("Destroy(gameObject.GetComponent<SphereBehaviour>().psystem)",0.5f);

            destroyer.DestroyGameObjectAfterDelay(0.5f);

            player.GetComponent<PlayerScore>().incrementScore(100);

        }

    }
}
