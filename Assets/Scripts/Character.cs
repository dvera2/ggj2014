using UnityEngine;
using System;

public class Character : MonoBehaviour {

    public enum Size {
        SMALL, MEDIUM, LARGE
    }

    public float jumpSpeedSmall = 8.5f;
    public float moveSpeedSmall = 5f;
    public float jumpSpeedMedium = 6.5f;
    public float moveSpeedMedium = 4f;
    public float jumpSpeedLarge = 5f;
    public float moveSpeedLarge = 2.5f;
    float boxSizeSmall = 0.9f;
    float boxSizeMedium = 1.9f;
    float boxSizeLarge = 2.9f;
    float resizeRate = 0.01f;


    public float baseHeight;

    float moveSpeed;
    float jumpSpeed;
    float facing = 1; // 1 or -1

    Vector2 moveDir;
    bool resizing = false;
    bool gettingbigger = false;

	public AudioClip jumpAudio;
	public AudioClip growAudio;
	public AudioClip shrinkAudio;

    void Start()
    {
        moveSpeed = moveSpeedSmall;
        jumpSpeed = jumpSpeedSmall;
		//GameObject.Instantiate(checkpointTemplate, new Vector3(transform.position.x - 2, transform.position.y - 2, transform.position.z), Quaternion.identity);
    }
    public Size size;

    void Update()
    {
		// check input for first time jump is hit for audio to play
		if (Input.GetButtonDown ("Jump") && Math.Abs(rigidbody2D.velocity.y) < 0.1f) 
		{
			GetComponent<AudioSource>().PlayOneShot(jumpAudio);
		}

        if (gameObject.tag == "Player")
        {
            move(InputManager.HorizAxis);
            if (InputManager.JumpDown) jump();
            if (InputManager.RestartDown) SpawnScript.levelReset();
        }

        if (gameObject.rigidbody2D.velocity.x > 0) facing = 1f;
        else if (gameObject.rigidbody2D.velocity.x < 0) facing = -1f;

        Transform b = gameObject.transform;

        if (resizing)
        {
            if (gettingbigger) {
				b.transform.localScale = new Vector2((Math.Abs(b.transform.localScale.x) + resizeRate) * facing, b.transform.localScale.y + resizeRate);				
			}
            else {
				b.transform.localScale = new Vector2((Math.Abs(b.transform.localScale.x) - resizeRate) * facing, b.transform.localScale.y - resizeRate);
			}
            baseHeight = transform.position.y - (b.transform.localScale.y / 2);

            if (size == Size.LARGE && Math.Abs(b.transform.localScale.x) >= boxSizeLarge)
            {
                resizing = false;
                b.transform.localScale = new Vector2(boxSizeLarge * facing, boxSizeLarge);
            }
            else if (size == Size.MEDIUM && gettingbigger && Math.Abs(b.transform.localScale.x) >= boxSizeMedium)
            {
                resizing = false;
                b.transform.localScale = new Vector2(boxSizeMedium * facing, boxSizeMedium);
            }
            else if (size == Size.MEDIUM && !gettingbigger && Math.Abs(b.transform.localScale.x) <= boxSizeMedium)
            {
                resizing = false;
                b.transform.localScale = new Vector2(boxSizeMedium * facing, boxSizeMedium);
            }
            else if (size == Size.SMALL && Math.Abs(b.transform.localScale.x) <= boxSizeSmall)
            {
                resizing = false;
                b.transform.localScale = new Vector2(boxSizeSmall * facing, boxSizeSmall);
            }

        }
        else
        {
            b.transform.localScale = new Vector2((Math.Abs(b.transform.localScale.x)) * facing, b.transform.localScale.y);
        }

        //gameObject.transform.localScale = new Vector2(Math.Abs(gameObject.transform.localScale.x * facing), gameObject.transform.localScale.y);
        
        baseHeight = transform.position.y - (b.transform.localScale.y / 2) + 0.01f;
    }

    public void move(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed * moveSpeed, rigidbody2D.velocity.y);
    }

    public void jump()
    {
        if (rigidbody2D.velocity.y < jumpSpeed / 4) rigidbody2D.velocity += new Vector2(0f, jumpSpeed) / 4;
        else rigidbody2D.velocity += new Vector2(0f, jumpSpeed) * Time.deltaTime * 3.2f;
    }

    public void changeSize(Size s)
    {
        if (s == Size.LARGE)
        {
            moveSpeed = moveSpeedLarge;
            jumpSpeed = jumpSpeedLarge;
            // TODO: call animation
            resizing = true;
            gettingbigger = true;
        }
        else if (s == Size.MEDIUM)
        {
            moveSpeed = moveSpeedMedium;
            jumpSpeed = jumpSpeedMedium;
            // TODO: call animation
            resizing = true;
            if (size == Size.SMALL) gettingbigger = true;
            else gettingbigger = false;
        }
        else if (s == Size.SMALL)
        {
            moveSpeed = moveSpeedSmall;
            jumpSpeed = jumpSpeedSmall;
            // TODO: call animation
            resizing = true;
            gettingbigger = false;
        }
        size = s;
        
    }

    public void makeThinner()
    {
		Debug.Log ("Shrinking..");
        if (size == Size.LARGE) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.SMALL);
        else if (size == Size.SMALL) GetComponent<PlayerDeath>().die();

		GetComponent<AudioSource>().PlayOneShot(shrinkAudio);
    }

    public void makeFatter()
    {
		Debug.Log ("Growing..");
        if (size == Size.SMALL) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.LARGE);
        else if (size == Size.LARGE) GetComponent<PlayerDeath>().die();

		GetComponent<AudioSource>().PlayOneShot(growAudio);
    }

    public void forceSmall()
    {
        size = Size.SMALL;
        resizing = false;
        gameObject.transform.localScale = new Vector2(boxSizeSmall * facing, boxSizeSmall);
        moveSpeed = moveSpeedSmall;
        jumpSpeed = jumpSpeedSmall;
    }
}
