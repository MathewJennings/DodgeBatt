﻿using UnityEngine;
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

    public void destroyFloatingHand()
    {
        Transform l = transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
                .FindChild("LeapHandController").FindChild("CapsuleHand_L");
        foreach (Transform child in l)
        {
            GameObject.Destroy(child.gameObject);
        }
        Transform r = transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
            .FindChild("LeapHandController").FindChild("CapsuleHand_R");
        foreach (Transform child in r)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
