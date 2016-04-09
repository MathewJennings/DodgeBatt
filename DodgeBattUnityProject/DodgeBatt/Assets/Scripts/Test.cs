using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : NetworkBehaviour {

    [SyncVar]
    private Color myColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.FindChild("Capsule").gameObject.GetComponent<Renderer>().material.color = myColor;

        if (!isLocalPlayer)
        {
            transform.FindChild("CenterEyeAnchor").gameObject.GetComponent<Camera>().gameObject.SetActive(false);
            return;
        }

        float x = Input.GetAxis("Horizontal") * 0.1f;
        float z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, 0, z);
	}

    public void setColor(Color c)
    {
        myColor = c;
    }
}
