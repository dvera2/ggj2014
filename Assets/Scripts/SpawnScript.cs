using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public static GameObject player;
    public static Vector3 spawnpos;

    public Transform checkpointTemplate;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		//player.transform.position = (transform.position);
        GameObject.Instantiate(checkpointTemplate, new Vector3(transform.position.x - 2, transform.position.y - 2, transform.position.z), Quaternion.identity);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

	public static void levelReset() {
        player.transform.position = new Vector3(spawnpos.x, spawnpos.y, player.transform.position.z);
        player.renderer.enabled = true;
        player.gameObject.GetComponent<Character>().forceSmall();
        player.gameObject.GetComponent<PlayerDeath>().alive = true;
        PickupListener[] candies = GameObject.FindObjectsOfType<PickupListener>();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponentInChildren<SchnopAnimController>().actionState = SchnopAnimController.ActionState.Walk;
        }
        foreach (PickupListener candy in candies)
        {
            candy.refresh();
        }

        CrumblingPlatform[] platforms = GameObject.FindObjectsOfType<CrumblingPlatform>();
        foreach (CrumblingPlatform platform in platforms)
        {
            platform.refresh();
        }
	}
}
