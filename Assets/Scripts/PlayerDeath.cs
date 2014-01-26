using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour {
    public bool alive = true;

	// Update is called once per frame
	void Update () {
        if (transform.position.y < -15f) die();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Trap") die();
            else if (contact.collider.tag == "Enemy" && Math.Abs(contact.normal.x) > contact.normal.y) die();
        }
    }

    public void die()
    {
        if(alive)
        {
            alive = false;
			//@Vishnu- Sets the player's position to the last checkpoint
			SpawnScript.levelReset();
        }
    }
}
