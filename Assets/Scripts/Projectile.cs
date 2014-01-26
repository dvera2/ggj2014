using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = -15f;

	// Use this for initialization
	void Start () {
        rigidbody2D.velocity = new Vector2(speed, Random.Range(0f, 10f));
	}

    void Update()
    {
        if (System.Math.Abs(rigidbody2D.velocity.x) < .005f)
            Destroy(gameObject);
    }
}
