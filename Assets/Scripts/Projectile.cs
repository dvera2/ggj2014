using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f;
    public GameObject particleSystemPrefab;

	// Use this for initialization
	void Start () {
        rigidbody2D.velocity = new Vector2(speed, Random.Range(0f, 30f));
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player")
            {
                GameObject particles = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity) as GameObject;
                particles.GetComponent<ParticleSystem>().Emit(50);
                particles.GetComponent<ParticleSystem>().renderer.sortingLayerName = "particles";
                Destroy(particles, 0.5f);
                collider.GetComponent<PlayerDeath>().die();
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (rigidbody2D.velocity.x < .005f)
            Destroy(gameObject);
    }
}
