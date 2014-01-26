using UnityEngine;
using System;

public class AIController : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT
    }

    public enum Behavior
    {
        PACE, WALKFORWARD, CHASE, RUN
    }

    private Character character;
    public Direction direction = Direction.LEFT;
    public Behavior behavior = Behavior.PACE;
    public float paceDistance = 0.5f;
    private float startX;
    public GameObject particleSystemPrefab;
    private GameObject activeParticles;

	// Use this for initialization
    void Start()
    {
        character = GetComponent<Character>();
        startX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float movement = 1;
        if (direction == Direction.LEFT) movement = -1;
        if (behavior != Behavior.PACE) movement *= 1.5f;
        if (Math.Abs(transform.position.x - GameObject.Find("Player").transform.position.x) < 30f)
            character.move(movement);

        if (behavior == Behavior.WALKFORWARD)
        {

        }
        else if (behavior == Behavior.PACE)
        {
            if (startX - transform.position.x >= paceDistance)
            {
                direction = Direction.RIGHT;
            }
            else if (transform.position.x - startX >= paceDistance)
            {
                direction = Direction.LEFT;
            }
        }
        else if(behavior == Behavior.CHASE)
        {
            if(transform.position.x - GameObject.Find("Player").transform.position.x > 0)
            {
                direction = Direction.LEFT;
            }
            else
            {
                direction = Direction.RIGHT;
            }
        }
        else if (behavior == Behavior.RUN)
        {
            if (transform.position.x - GameObject.Find("Player").transform.position.x < 0)
            {
                direction = Direction.LEFT;
            }
            else
            {
                direction = Direction.RIGHT;
            }
        }
        if (GameObject.Find("Player").GetComponent<Character>().size == Character.Size.LARGE)
            GetComponentInChildren<SchnopAnimController>().emoState = SchnopAnimController.EmoState.Scared;
        if (GameObject.Find("Player").GetComponent<Character>().size == Character.Size.MEDIUM)
            GetComponentInChildren<SchnopAnimController>().emoState = SchnopAnimController.EmoState.Neutral;
        if (GameObject.Find("Player").GetComponent<Character>().size == Character.Size.SMALL)
            GetComponentInChildren<SchnopAnimController>().emoState = SchnopAnimController.EmoState.Mad;

        if (Math.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) < 8f &&
            GameObject.Find("Player").GetComponent<Character>().size == Character.Size.LARGE) behavior = Behavior.RUN;
        else if (Math.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) < 4f) behavior = Behavior.CHASE;
        else behavior = Behavior.PACE;
	}

    void OnDisable()
    {
        if (activeParticles != null)
            Destroy(activeParticles);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.tag == "Player" && contact.collider.GetComponent<Character>().baseHeight > transform.position.y)
            {
                if (activeParticles != null)
                    Destroy(activeParticles);
                activeParticles = Instantiate(particleSystemPrefab, contact.point, Quaternion.identity) as GameObject;
                activeParticles.GetComponent<ParticleSystem>().Emit(50);
                activeParticles.GetComponent<ParticleSystem>().renderer.sortingLayerName = "particles";
                contact.collider.GetComponent<Character>().jump();
                Character.Size otherSize = contact.collider.GetComponent<Character>().size;
                Character.Size size = GetComponent<Character>().size;
                if (size == otherSize || otherSize == Character.Size.LARGE || size == Character.Size.SMALL)
                {
                    GetComponentInChildren<SchnopAnimController>().actionState = SchnopAnimController.ActionState.Die;
                    GameObject.Destroy(gameObject, .5f);
                }
            }
            else if (contact.collider.tag == "Player")
            {
                GetComponentInChildren<SchnopAnimController>().actionState = SchnopAnimController.ActionState.Hit;
            }
        }
    }
}
