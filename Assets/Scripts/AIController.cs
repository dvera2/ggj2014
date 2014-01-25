using UnityEngine;
using System;

public class AIController : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT
    }

    private Character character;
    public Direction direction = Direction.LEFT;
    public float paceDistance = 5f;
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
        float movement = GetComponent<Character>().moveSpeed;
        if(direction == Direction.LEFT)
        {
            movement = -movement;
        }
        character.move(movement);
        if (startX - transform.position.x >= paceDistance)
        {
            direction = Direction.RIGHT;
        }
        else if (transform.position.x - startX >= paceDistance)
        {
            direction = Direction.LEFT;
        }
	}
}
