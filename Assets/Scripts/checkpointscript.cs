using UnityEngine;
using System.Collections;

/*
	This class represents a checkpoint. When the player makes contact with it,
	it saves their location. In the event of death/level reset, the player's
	location is set to the location of the CheckPoint prefab
 */
public class checkpointscript : MonoBehaviour {

	public GameObject player; //Player
	public bool isReached; //Whether the checkpoint has been reached

	// Use this for initialization
	void Start () {
		//Instantiate vars
		player = GameObject.FindGameObjectWithTag("Player");
		//Init variables
		isReached = false;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
		//Check if player overlaps with checkpoint
		//Debug.Log(player.transform.position);
		//if so, and the checkpoint hasn't already been used, set it as the proper checkpoint
		if (player != null && !isReached && player.transform.position.x >= transform.position.x) {
			isReached = true; //set reached bool to true
			SpawnScript.spawnpos = new Vector3(transform.position.x, transform.position.y, transform.position.z); //Set SpawnScript's spawnpos to this CheckPoint's position
			Debug.Log("Checkpoint Reached!"); 
		}
	}
}
