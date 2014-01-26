using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public static GameObject player;
	public static Vector3 spawnpos;

	// Use this for initialization
	void Start () {
		Debug.Log ("SpawnScript running!");
		player = GameObject.FindGameObjectWithTag ("Player");
		player.transform.position = (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		//player.transform.TransformPoint (transform.position);
	}

	/**
		Called when the player dies or the level is reset;
		sets the player's position to spawnpos
	 */
	public static void levelReset(){
		player.transform.position = spawnpos;
	}
}
