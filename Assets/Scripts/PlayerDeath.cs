using UnityEngine;

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
            if (contact.collider.tag == "Enemy" || contact.collider.tag == "Trap")
                die();
        }
    }

    public void die()
    {
        if(alive)
        {
            alive = false;
        }
    }
}
