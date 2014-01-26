using UnityEngine;

public class Boss : MonoBehaviour {
    public float projectileDelay = 2f;
    private float delayTimer = 0f;
    public Transform projectile;
    private bool dying = false;
    private Vector2 velocity = new Vector2(0f, 0f);
	
    // Update is called once per frame
    void Update () {
        delayTimer += Time.deltaTime;
        if(!dying && delayTimer >= projectileDelay)
        {
            delayTimer = 0f;
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
        else if (dying)
        {
            velocity.x += Random.Range(-.4f, .4f);
            transform.Translate(velocity);
        }
	}

    void OnTiggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            velocity.y = -3f;
            dying = true;
        }
    }
}
