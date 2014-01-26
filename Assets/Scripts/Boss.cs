using UnityEngine;

public class Boss : MonoBehaviour {
    public float projectileDelay = 2f;
    private float delayTimer = -3f;
    public Transform projectile;
    private bool dying = false;
    private Vector2 velocity = new Vector2(0f, 0f);
	
    // Update is called once per frame
    void Update () {
        delayTimer += Time.deltaTime;
        if(!dying && delayTimer >= projectileDelay)
        {
            delayTimer = 0f;
            Instantiate(projectile, new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, 0f), Quaternion.identity);
        }
        else if (dying)
        {
            velocity.x += Random.Range(-.4f, .4f);
            transform.Translate(velocity);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player" && collider.name != "Projectile")
        {
            velocity.y = -3f;
            dying = true;
            (GameObject.FindGameObjectWithTag("Player").GetComponent<Character>()).forceSmall();
        }
    }
}
