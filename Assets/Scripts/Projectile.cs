using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f;

	// Use this for initialization
	void Start () {
        rigidbody2D.velocity = new Vector2(speed, Random.Range(0f, 5f));
	}

    void Update()
    {
        if (rigidbody2D.velocity.x < .005f)
            Destroy(gameObject);
    }
}
