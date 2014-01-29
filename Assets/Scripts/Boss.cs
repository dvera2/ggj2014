using UnityEngine;

public class Boss : MonoBehaviour {
    public float projectileDelay = 2f;
    private float delayTimer = -3f;
    public Transform projectile;
    private bool dying = false;
    private Vector2 velocity = new Vector2(0f, 0f);

	public AudioClip spitSound;
	
    // Update is called once per frame
    void Update () {
        delayTimer += Time.deltaTime;
        if(!dying && delayTimer >= projectileDelay)
        {
            delayTimer = 0f;
            Instantiate(projectile, new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, 0f), Quaternion.identity);
            GetComponentInChildren<CthjujuAnimController>().actionState = CthjujuAnimController.ActionState.Attack;

			GetComponent<AudioSource>().PlayOneShot(spitSound);
        }
        if (!dying && delayTimer >= projectileDelay / 4)
        {
            GetComponentInChildren<CthjujuAnimController>().actionState = CthjujuAnimController.ActionState.Idle;
        }
        else if (dying)
        {;
            transform.Translate(velocity);
			Camera.main.camera.GetComponent<CameraFollow>().target = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player" && collider.name != "Projectile")
        {
            velocity.y = -.01f;
            dying = true;
            (GameObject.FindGameObjectWithTag("Player").GetComponent<Character>()).forceSmall();
            GetComponentInChildren<CthjujuAnimController>().actionState = CthjujuAnimController.ActionState.Die;
        }
        else if(collider.tag == "Player")
        {
            collider.GetComponent<PlayerDeath>().die();
        }
    }
}
