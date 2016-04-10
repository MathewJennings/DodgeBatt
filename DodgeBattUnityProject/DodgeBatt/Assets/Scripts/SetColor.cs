using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SetColor : NetworkBehaviour {

    public Material orangeMat;
    public Material blueMat;
    public GameObject model;

    [SyncVar]
    private Material mat;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        MeshRenderer rend = model.GetComponent<MeshRenderer>() as MeshRenderer;
        rend.material = mat;
    }

    public void turnBlue()
    {
        mat = blueMat;
    }

    public void turnOrange()
    {
        mat = orangeMat;
    }
}
