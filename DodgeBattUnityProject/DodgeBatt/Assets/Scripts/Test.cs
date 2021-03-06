﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : NetworkBehaviour {

    [SyncVar]
    private Color myColor;

	private int hp = 100;

	// Use this for initialization
	void Start () {
        CmdDestroyFloatingHand();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            transform.FindChild("CenterEyeAnchor").gameObject.GetComponent<Camera>().gameObject.SetActive(false);
            return;
        }

        //float x = Input.GetAxis("Horizontal") * 0.1f;
        //float z = Input.GetAxis("Vertical") * 0.1f;

        //transform.Translate(x, 0, z);
	}

    public void setColor(Color c)
    {
        myColor = c;
    }

    public Color getColor()
    {
        return myColor;
    }

    [ClientRpc]
    public void RpcDestroyFloatingHand()
    {
        Transform l = transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
                 .FindChild("LeapHandController").FindChild("CapsuleHand_L");
        foreach (Transform child in l)
        {
            if (!child.gameObject.tag.Equals("Palm_Tracker"))
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        Transform r = transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
            .FindChild("LeapHandController").FindChild("CapsuleHand_R");
        foreach (Transform child in r)
        {
            if (!child.gameObject.tag.Equals("Palm_Tracker"))
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

	public void DecerementHP(int amt) {
		hp -= amt;
		if (hp < 0) {
			hp = 0;
		}
	}

	public int GetHP () {
		return hp;
	}

    [Command]
    public void CmdDestroyFloatingHand()
    {
        RpcDestroyFloatingHand();
    }
}
