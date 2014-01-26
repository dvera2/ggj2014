using UnityEngine;

public class Crusher : MonoBehaviour {
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public Direction direction = Direction.UP;
    public float speed = .5f;
    public float delay = 2f;
    private float crushTimer = 0f;
	
	// Update is called once per frame
    void Update()
    {
        crushTimer += Time.deltaTime;
        if(crushTimer >= delay)
        {
            if (direction == Direction.UP) transform.Translate(0f, Time.deltaTime / speed, 0f);
            if (direction == Direction.DOWN) transform.Translate(0f, -(Time.deltaTime / speed), 0f);
            if (direction == Direction.LEFT) transform.Translate(0f, -(Time.deltaTime / speed), 0f);
            if (direction == Direction.RIGHT) transform.Translate(0f, Time.deltaTime / speed, 0f);
        }
        else if(crushTimer >= speed + delay)
        {
            crushTimer = 0f;
        }
	}
}
