using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onStartClick(){
		Application.LoadLevel ("MainScene");
	}
	public void onExitClick(){
		Application.Quit();
	}
}
