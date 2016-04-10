using UnityEngine;
using System.Collections;

public class AsteroidContact : MonoBehaviour {

    public GameObject hundredPointsUI;

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
            // Create a vector just above the asteroid for a score popup.
            Vector3 scorePos;
            scorePos = transform.position;
            scorePos.y += 1f;

            // Instantiate the 100 points prefab at this point.
            Instantiate(hundredPointsUI, scorePos, Quaternion.identity);

            destroyer.DestroyGameObjectAfterDelay(0.5f);
        }

    }
}
