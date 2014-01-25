using UnityEngine;
using System;

public class AIController : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT
    }

    private Character character;
    public float speed = 2f;
    public Direction direction = Direction.LEFT;
    public float paceDistance = 10f;
    private float currentPaceDistance = 0f;

	// Use this for initialization
    void Start()
    {
        character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float movement = speed * Time.deltaTime;
        if(direction == Direction.LEFT)
        {
            movement = Math.Max(-movement, -(paceDistance - currentPaceDistance));
        }
        else
        {
            movement = Math.Min(movement, paceDistance - currentPaceDistance);
        }
        currentPaceDistance += movement;
        character.move(speed);
	}
}
