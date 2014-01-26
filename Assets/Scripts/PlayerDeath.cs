using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour {
    public bool alive = true;
    int respawnCount = 0;

	// Update is called once per frame
	void Update () {
        if (transform.position.y < -15f)
        {
            SpawnScript.levelReset();
        }
        if (!alive)
        {
            respawnCount++;
            if (respawnCount == 90)
            {
                respawnCount = 0;
                SpawnScript.levelReset();
                alive = true;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Trap") die();
            else if (contact.collider.tag == "Enemy" && contact.collider.transform.position.y > GetComponent<Character>().baseHeight) die();
        }
    }

    public void die()
    {
        if(alive)
        {
            alive = false;
            gameObject.renderer.enabled = false;
			//SpawnScript.levelReset();
        }
    }
}
