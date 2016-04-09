using UnityEngine;
using System.Collections.Generic;
using Leap;

public class Bat : MonoBehaviour {
    
    public GameObject batPrefab;

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
        if (rightHand != null)
        {
            CheckAndUpdateBat(rightHand, false);
        }
    }

    private void CheckAndUpdateBat(Hand hand, bool isLeft)
    {
        Vector3 reference = new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z);
        Debug.Log(hand.PalmPosition);
        reference.x /= 1000;
        reference.y /= 1000;
        reference.z /= 1000;
        //reference = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0) * reference;
        Vector3 handAngle = new Vector3(hand.PalmNormal.x, hand.PalmNormal.y, hand.PalmNormal.z);
        handAngle.x /= 1000;
        handAngle.y /= 1000;
        handAngle.z /= 1000;
        //handAngle = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0) * handAngle;
        Vector3 swordPosition = reference;

        Vector3 palmNormal = new Vector3(hand.PalmNormal.x, hand.PalmNormal.y, hand.PalmNormal.z);
        //palmNormal.z *= -1;
        Vector3 palmDirection = new Vector3(hand.Direction.x, hand.Direction.y, hand.Direction.z);
        //palmDirection.z *= -1;
        Vector3 batDirection = Vector3.Cross(palmNormal, palmDirection);

        if (isLeft)
        {
            if (hand.IsLeft && hand.GrabStrength > .1)
            {

                if (batLeft == null)
                {
                    batLeft = (GameObject)Instantiate(batPrefab, swordPosition, Quaternion.identity);
                    batLeft.transform.rotation = Quaternion.FromToRotation(Vector3.down, batDirection);
                }
                else
                {
                    //batLeft.GetComponent<ObjectScript>().BeginFadeIn();
                    batLeft.GetComponent<Transform>().position = swordPosition;
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
                    batRight = (GameObject)Instantiate(batPrefab, swordPosition, Quaternion.identity);
                    batRight.transform.rotation = Quaternion.FromToRotation(Vector3.up, batDirection);
                }
                else
                {
                    //batRight.GetComponent<ObjectScript>().BeginFadeIn();
                    batRight.GetComponent<Transform>().position = swordPosition;
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
}
