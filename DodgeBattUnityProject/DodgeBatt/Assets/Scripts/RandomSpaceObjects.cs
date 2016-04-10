using UnityEngine;
using System.Collections;

public class RandomSpaceObjects : MonoBehaviour {

    public GameObject junkPrefab;
    public GameObject spiralPrefab;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 10; i++)
        {
            float s = Random.Range(0f, 2 * Mathf.PI);
            float t = Random.Range(0f, 2 * Mathf.PI);
            float x = 70 * Mathf.Cos(s) * Mathf.Sin(t);
            float y = 70 * Mathf.Sin(s) * Mathf.Sin(t);
            float z = 70 * Mathf.Cos(t);

            Instantiate(spiralPrefab, new Vector3(x, y, z), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
