using UnityEngine;
using System.Collections;

public class BallMaterial : MonoBehaviour {

    public Material orangeMat;
    public Material blueMat;
    public GameObject model;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnBlue()
    {
        MeshRenderer rend = model.GetComponent<MeshRenderer>() as MeshRenderer;
        rend.material = blueMat;
    }

    public void turnOrange()
    {
        MeshRenderer rend = model.GetComponent<MeshRenderer>() as MeshRenderer;
        rend.material = orangeMat;
    }
}
