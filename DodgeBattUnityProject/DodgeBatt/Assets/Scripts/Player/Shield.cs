using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Shield : MonoBehaviour {

    public GameObject shieldPrefab;
    public float normalScalar = 0.4f;

    Controller controller;
    GameObject centerEyeAnchor;
    public GameObject leapControllerGameObj;
    GameObject leftShield;
    GameObject rightShield;

    void Start () {
        controller = new Controller();
        leapControllerGameObj = GameObject.Find("LeapHandController");
        centerEyeAnchor = GameObject.Find("CenterEyeAnchor");

    }
	
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
            }
            else
            {
                rightHand = h;
            }
        }

        if (leftHand != null)
        {
            CheckAndUpdateShield(leftHand, true);
        }
        else if (leftShield != null)  // && leftHand == null
        {
            Destroy(leftShield);
        }

        if (rightHand != null)
        {
            CheckAndUpdateShield(rightHand, false);
        }
        else if (rightShield != null) // && rightHand == null
        {
            Destroy(rightShield);
        }
    }



    private void CheckAndUpdateShield(Hand hand, bool isLeftHand)
    {
        // making the gesture.
        if (Mathf.Abs(hand.PalmNormal.y) > 0.9 && hand.Direction.z < -0.9)
        {
            // Get the rotation of the hand.
            Vector3 palmNormal = toVector3(hand.PalmNormal);
            Vector3 palmDirection = toVector3(hand.Direction);
            Vector3 shieldRotation = Vector3.Cross(palmNormal, palmDirection);

            Debug.Log("leapControllerPos" + leapControllerGameObj.transform.position);
            Vector3 targetPosition = leapControllerGameObj.transform.position + toVector3(hand.PalmPosition) / 1000 + toVector3(hand.PalmNormal) * normalScalar;
            targetPosition = Quaternion.Euler(0, centerEyeAnchor.transform.rotation.eulerAngles.y, 0) * targetPosition;
            
            Debug.Log("palm: " + hand.PalmNormal);
            if (isLeftHand)
            {
                if (leftShield == null)
                {
                    // Make the left shield.
                    leftShield = (GameObject)Instantiate(shieldPrefab, targetPosition, Quaternion.identity);
                    leftShield.transform.rotation = Quaternion.FromToRotation(Vector3.down, shieldRotation);
                    leftShield.transform.rotation = Quaternion.Euler(0, 90, 0) * leftShield.transform.rotation;
                }
                else
                {
                    // Maintain the left shield.
                    leftShield.GetComponent<Transform>().position = targetPosition;
                    leftShield.transform.rotation = Quaternion.FromToRotation(Vector3.down, shieldRotation);
                    leftShield.transform.rotation = Quaternion.Euler(0, 90, 0) * leftShield.transform.rotation;
                }
            }
            else
            {
                if (rightShield == null)
                {
                    // Make the right shield.
                    rightShield = (GameObject)Instantiate(shieldPrefab, targetPosition, Quaternion.identity);
                    rightShield.transform.rotation = Quaternion.FromToRotation(Vector3.down, shieldRotation);
                    rightShield.transform.rotation = Quaternion.Euler(90, 0, 0) * rightShield.transform.rotation;
                }
                else
                {
                    // Maintain the right shield.
                    rightShield.GetComponent<Transform>().position = targetPosition;
                    rightShield.transform.rotation = Quaternion.FromToRotation(Vector3.down, shieldRotation);
                    rightShield.transform.rotation = Quaternion.Euler(90, 0, 0) * rightShield.transform.rotation;
                }
            }
        } 
        else if (isLeftHand && leftShield != null)
        {
            Destroy(leftShield);
        }
        else if (!isLeftHand && rightShield != null)
        {
            Destroy(rightShield);
        }
    }



    /*********************************************************HELPER FUNCTIONS*************************************************************************************/

    private Vector3 toVector3(Vector v)
    {
        Vector3 v3 = v.ToVector3();

        return new Vector3(-v3.x, -v3.z, v3.y);
    }

    private Vector3 toVector3Scaled(Vector v, float scale)
    {
        return new Vector3(v.x * scale, v.y * scale, v.z * scale);
    }

}
