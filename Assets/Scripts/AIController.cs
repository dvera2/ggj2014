using UnityEngine;
using System;

public class AIController : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT
    }

    public enum Behavior
    {
        PACE, WALKFORWARD, CHASE
    }

    private Character character;
    public Direction direction = Direction.LEFT;
    public Behavior behavior = Behavior.PACE;
    public float paceDistance = 0.5f;
    private float startX;

	// Use this for initialization
    void Start()
    {
        character = GetComponent<Character>();
        startX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float movement = 1;
        if (direction == Direction.LEFT) movement = -1;
        if (Math.Abs(transform.position.x - GameObject.Find("Player").transform.position.x) < 30f)
            character.move(movement);

        if (behavior == Behavior.WALKFORWARD)
        {

        }
        else if (behavior == Behavior.PACE)
        {
            if (startX - transform.position.x >= paceDistance)
            {
                direction = Direction.RIGHT;
            }
            else if (transform.position.x - startX >= paceDistance)
            {
                direction = Direction.LEFT;
            }
        }
        else if(behavior == Behavior.CHASE)
        {
            if(transform.position.x - GameObject.Find("Player").transform.position.x > 0)
            {
                direction = Direction.LEFT;
            }
            else
            {
                direction = Direction.RIGHT;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && Math.Abs(contact.normal.x) < -contact.normal.y)
            {
                GameObject.Destroy(gameObject);
                contact.collider.GetComponent<Character>().jump();
            }
        }
    }
}
