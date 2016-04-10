using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Bat : NetworkBehaviour {
    
    public GameObject batPrefab;
    public float normalScalar = 1.0f;
    public GameObject controller_;
    public Shield shield;

    Controller controller;

    [SyncVar]
    GameObject batLeft;

    [SyncVar]
    GameObject batRight;

    Transform leftPalm, rightPalm;

    bool playerIsBlue; // Color of the player holding this bat.

    // Use this for initialization
    void Start () {
        controller = new Controller();
        batLeft = null;
        batRight = null;
        playerIsBlue = gameObject.GetComponent<Test>().getColor().Equals(Color.blue);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;

        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        Hand leftHand = null;
        Hand rightHand = null;
        // Assuming there are only two hands in the scene.
        foreach (Hand h in hands)
        {
            if (h.IsLeft)
            {
                leftHand = h;
                leftPalm = controller_.transform.FindChild("RigidRoundHand_L").FindChild("palm");
            } else
            {
                rightHand = h;
                rightPalm = controller_.transform.FindChild("RigidRoundHand_R").FindChild("palm");
            }
        }

        if (leftHand != null)
        {
            CheckAndUpdateBat(leftHand, true);
        }
        else if (batLeft != null)  // && leftHand == null
        {
            CmdDestroyBat(true);
        }

        if (rightHand != null)
        {
            CheckAndUpdateBat(rightHand, false);
        }
        else if (batRight != null)  // && rightHand == null
        {
            CmdDestroyBat(false);
        }
    }

    private void CheckAndUpdateBat(Hand hand, bool isLeft)
    {
        Vector3 batPosition;
        if (isLeft)
        {
            batPosition = leftPalm.position - swappedYZVector(toVector3Scaled(hand.Direction, 0.02f));
        }
        else
        {
            batPosition = rightPalm.position - swappedYZVector(toVector3Scaled(hand.Direction, 0.02f));
        }
        Vector3 pn = hand.PalmNormal.ToVector3();
        pn = swappedYZVector(pn);
        Vector3 pd = hand.Direction.ToVector3();
        pd = swappedYZVector(pd);
        //Vector3 batDirection = Vector3.Cross(pd, pn);
        Vector3 batDirection = new Vector3(pd.x, pd.y, pd.z);

        if (isLeft)
        {
            if (hand.GrabStrength > 0.4f)
            {

                batDirection = new Vector3(-pd.x, -pd.y, pd.z);
                if (batLeft == null && !shield.leftShieldExists())
                {
                    CmdSpawnBat(batPosition, batDirection, true);
                }
                else if (leftBatExists())
                {
                    CmdUpdateBat(batPosition, batDirection, true);
                }
            }
            else if (batLeft != null)
            {
                CmdDestroyBat(true);
            }
        }
        else
        {
            if (hand.GrabStrength > 0.4f)
            {

                batDirection = new Vector3(-pd.x, -pd.y, pd.z);
                if (batRight == null && !shield.rightShieldExists())
                {
                    CmdSpawnBat(batPosition, batDirection, false);
                }
                else if (rightBatExists())
                {
                    CmdUpdateBat(batPosition, batDirection, false);
                }
            }
            else if (batRight != null)
            {
                CmdDestroyBat(false);
            }
        }
    }

    [Command]
    void CmdSpawnBat(Vector3 batPosition, Vector3 batDirection, bool isLeftBat)
    {
        if (isLeftBat && batLeft != null)
        {
            return;
        }
        if (!isLeftBat && batRight != null)
        {
            return;
        }
        // This [Command] code is run on the server!
        // create the bat object locally on the server
        GameObject bat = (GameObject)Instantiate(batPrefab, batPosition, Quaternion.identity);
        bat.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
        setBatColor(bat);
        // spawn the bat on the clients
        NetworkServer.SpawnWithClientAuthority(bat, connectionToClient);
        if (isLeftBat)
        {
            batLeft = bat;
        }
        else
        {
            batRight = bat;
        }
    }

    [Command]
    void CmdUpdateBat(Vector3 batPosition, Vector3 batDirection, bool isLeftBat)
    {
        if (isLeftBat && batLeft != null)
        {
            batLeft.GetComponent<Transform>().position = batPosition;
            batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
        }
        else if (batRight != null)
        {
            batRight.GetComponent<Transform>().position = batPosition;
            batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
        }
    }

    [Command]
    void CmdDestroyBat(bool isLeftBat)
    {
        if (isLeftBat && batLeft != null)
        {
            Destroy(batLeft);
        }
        else if (batRight != null)
        {
            Destroy(batRight);
        }
    }

    private Vector3 toVector3Scaled(Vector v, float scale)
    {
        return new Vector3(v.x * scale, v.y * scale, v.z * scale);
    }

    private Vector3 swappedYZVector(Vector3 v)
    {
        return new Vector3(v.x, v.z, v.y);
    }

    public bool leftBatExists()
    {
        return batLeft != null;
    }

    public bool rightBatExists()
    {
        return batRight != null;
    }

    private void setBatColor(GameObject bat)
    {
        if (playerIsBlue)
        {
            bat.GetComponent<SetColor>().turnBlue();
        }
        else
        {
            bat.GetComponent<SetColor>().turnOrange();
        }
    }
}
