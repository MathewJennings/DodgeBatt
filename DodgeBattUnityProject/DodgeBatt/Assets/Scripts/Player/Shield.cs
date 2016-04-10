using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Shield : MonoBehaviour {

    public GameObject shieldPrefab;
    public float normalScalar = 0.4f;
    public Bat bat;

    Controller controller;
    public GameObject controller_;
    GameObject leftShield;
    GameObject rightShield;

    Transform leftPalm, rightPalm;

    void Start () {
        controller = new Controller();
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
                leftPalm = controller_.transform.FindChild("RigidRoundHand_L").FindChild("palm");
            }
            else
            {
                rightHand = h;
                rightPalm = controller_.transform.FindChild("RigidRoundHand_R").FindChild("palm");
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

            Vector3 shieldPosition;
            Quaternion shieldRotation;
            if (isLeftHand)
            {
                shieldPosition = leftPalm.position;
                shieldRotation = leftPalm.rotation;
            }
            else
            {
                shieldPosition = rightPalm.position;
                shieldRotation = rightPalm.rotation;
            }

            if (isLeftHand)
            {
                if (leftShield == null && !bat.leftBatExists())
                {
                    // Make the left shield.
                    leftShield = (GameObject)Instantiate(shieldPrefab, shieldPosition, shieldRotation);
                }
                else if (leftShieldExists())
                {
                    // Maintain the left shield.
                    leftShield.GetComponent<Transform>().position = shieldPosition + swappedYZVector(toVector3Scaled(hand.PalmNormal, 0.1f));
                    leftShield.GetComponent<Transform>().rotation = shieldRotation;
                }
            }
            else
            {
                if (rightShield == null && !bat.rightBatExists())
                {
                    // Make the right shield.
                    rightShield = (GameObject)Instantiate(shieldPrefab, shieldPosition, shieldRotation);
                }
                else if (rightShieldExists())
                {
                    // Maintain the right shield.
                    rightShield.GetComponent<Transform>().position = shieldPosition + swappedYZVector(toVector3Scaled(hand.PalmNormal, 0.1f));
                    rightShield.GetComponent<Transform>().rotation = shieldRotation;
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

    private Vector3 swappedYZVector(Vector3 v)
    {
        return new Vector3(v.x, v.z, v.y);
    }

    public bool leftShieldExists()
    {
        return leftShield != null;
    }

    public bool rightShieldExists()
    {
        return rightShield != null;
    }
}
