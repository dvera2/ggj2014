using UnityEngine;

public class Crusher : MonoBehaviour {
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public Direction direction = Direction.UP;
    public float speed = 1f;
    public float delay = 1f;
    private float crushTimer = 0f;
	
	// Update is called once per frame
    void Update()
    {
        crushTimer += Time.deltaTime;
        if (crushTimer >= 3 * delay) crushTimer = 0f;
        else if(crushTimer >= 2 * delay)
        {
            if (direction == Direction.UP) transform.Translate(0f, -Time.deltaTime * speed, 0f);
            if (direction == Direction.DOWN) transform.Translate(0f, (Time.deltaTime * speed), 0f);
            if (direction == Direction.LEFT) transform.Translate(0f, (Time.deltaTime * speed), 0f);
            if (direction == Direction.RIGHT) transform.Translate(0f, -Time.deltaTime * speed, 0f);
        }
        else if(crushTimer >= delay)
        {
            if (direction == Direction.UP) transform.Translate(0f, Time.deltaTime * speed, 0f);
            if (direction == Direction.DOWN) transform.Translate(0f, -(Time.deltaTime * speed), 0f);
            if (direction == Direction.LEFT) transform.Translate(0f, -(Time.deltaTime * speed), 0f);
            if (direction == Direction.RIGHT) transform.Translate(0f, Time.deltaTime * speed, 0f);
        }
	}
}
