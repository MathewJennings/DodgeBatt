using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public bool destroyOnAwake;         // Whether or not this gameobject should be destroyed after a delay, upon Awake.
    public float awakeDestroyDelay;     // The delay for destroying it on Awake.
    public bool findChild = false;      // Find a child game object and delete it.
    public string namedChild;           // Name the child object in Inspector.
    
    void Awake() {
	    if (destroyOnAwake) {
            if (findChild) {
                Destroy(transform.Find(namedChild).gameObject);
            } else {
                // Destroy the gameobject after the delay.
                Destroy(gameObject, awakeDestroyDelay);
            }
        }
	}
	
	void DestroyChildGameObject() {
        // This can be called from an Animation Event.
        if (transform.Find(namedChild).gameObject != null) {
            Destroy(transform.Find(namedChild).gameObject);
        }
    }

    void DisableChildGameObject() {
        // This can be called from an Animation Event.
        if (transform.Find(namedChild).gameObject.activeSelf == true) {
            transform.Find(namedChild).gameObject.SetActive(false);
        }
    }

    public void DestroyGameObjectAfterDelay(float delay) {
        Invoke("DestroyGameObject", delay);
    }

    public void DestroyGameObject() {
        // This can be called from an Animation Event.
        Destroy(gameObject);
    }

}
