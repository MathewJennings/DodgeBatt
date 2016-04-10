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
    GameObject batLeft;
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
            Destroy(batLeft);
        }

        if (rightHand != null)
        {
            CheckAndUpdateBat(rightHand, false);
        }
        else if (batRight != null)  // && rightHand == null
        {
            Destroy(batRight);
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

                batDirection = new Vector3(pd.x, pd.y, -pd.z);
                if (batLeft == null && !shield.leftShieldExists())
                {
                    batLeft = (GameObject)Instantiate(batPrefab, batPosition, Quaternion.identity);
                    batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                    setBatColor(batLeft);
                }
                else if (leftBatExists())
                {
                    batLeft.GetComponent<Transform>().position = batPosition;
                    batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                }
            }
            else if (batLeft != null)
            {
                Destroy(batLeft);
            }
        }
        else
        {
            if (hand.GrabStrength > 0.4f)
            {

                batDirection = new Vector3(-pd.x, -pd.y, pd.z);
                if (batRight == null && !shield.rightShieldExists())
                {
                    batRight = (GameObject)Instantiate(batPrefab, batPosition, Quaternion.identity);
                    batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                    setBatColor(batRight);
                }
                else if (rightBatExists())
                {
                    batRight.GetComponent<Transform>().position = batPosition;
                    batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                }
            }
            else if (batRight != null)
            {
                Destroy(batRight);
            }
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
