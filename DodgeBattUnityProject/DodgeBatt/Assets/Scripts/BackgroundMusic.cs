using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	AudioSource fxSound;
	public AudioClip backMusic; 

	// Use this for initialization
	void Start ()
	{
		// Audio Source responsavel por emitir os sons
		fxSound = GetComponent<AudioSource> ();
		fxSound.Play ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
