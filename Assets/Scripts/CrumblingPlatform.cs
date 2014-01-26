﻿using UnityEngine;
using System.Collections;

public class CrumblingPlatform : MonoBehaviour {

    public enum Strength
    {
        WEAK, MEDIUM
    }

    public Strength strength = Strength.WEAK;

	void Start () {
	    
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && contact.collider.GetComponent<Character>().baseHeight > transform.position.y
                && contact.collider.rigidbody2D.velocity.y <= 0)
            {
                if ((strength == Strength.MEDIUM && contact.collider.GetComponent<Character>().size == Character.Size.LARGE)
                    || (strength == Strength.WEAK && contact.collider.GetComponent<Character>().size != Character.Size.SMALL))
                {
                    // TODO: particles here
                    renderer.enabled = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    public void refresh()
    {
        renderer.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}