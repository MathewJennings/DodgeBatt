using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Bat : MonoBehaviour {
    
    public GameObject batPrefab;
    public float normalScalar = 1.0f;
    public GameObject controller_;

    Controller controller;
    GameObject batLeft;
    GameObject batRight;

    // Use this for initialization
	void Start () {
        controller = new Controller();
        batLeft = null;
        batRight = null;
	}
	
	// Update is called once per frame
	void Update () {
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
            } else
            {
                rightHand = h;
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
        Vector3 batPosition = toVector3Scaled(hand.PalmPosition, 0.001f);
        batPosition = controller_.transform.TransformPoint(batPosition);

        //controller_.transform.TransformDirection(local_dir);
        Vector3 batDirection = Vector3.zero;

        if (isLeft)
        {
            if (hand.IsLeft && hand.GrabStrength > 0.2f)
            {

                if (batLeft == null)
                {
                    batLeft = (GameObject)Instantiate(batPrefab, batPosition, Quaternion.identity);
                    batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                }
                else
                {
                    //batLeft.GetComponent<ObjectScript>().BeginFadeIn();
                    batLeft.GetComponent<Transform>().position = batPosition;
                    batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                }
            }
            else if (batLeft != null)
            {
                //batLeft.GetComponent<ObjectScript>().BeginFadeOut();
                //batLeft.GetComponent<Transform>().position = swordPosition;
                //batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                Destroy(batLeft);
            }
        }
        else
        {
            if (hand.IsRight && hand.GrabStrength > .1)
            {

                if (batRight == null)
                {
                    batRight = (GameObject)Instantiate(batPrefab, batPosition, Quaternion.identity);
                    batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                }
                else
                {
                    //batRight.GetComponent<ObjectScript>().BeginFadeIn();
                    batRight.GetComponent<Transform>().position = batPosition;
                    batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                }
            }
            else if (batRight != null)
            {
                //batRight.GetComponent<ObjectScript>().BeginFadeOut();
                //batRight.GetComponent<Transform>().position = swordPosition;
                //batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                Destroy(batRight);
            }
        }
    }

    private Vector3 toVector3Scaled(Vector v, float scale)
    {
        return new Vector3(v.x * scale, v.y * scale, v.z * scale);
    }
}
