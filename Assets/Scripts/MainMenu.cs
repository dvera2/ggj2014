using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 40), "Play")) 
		{
			//game.Start();
		}
	}
}
