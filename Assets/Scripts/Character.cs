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
    public float moveSpeedLarge = 2f;
    public float boxSizeSmall = 1f;
    public float boxSizeMedium = 2f;
    public float boxSizeLarge = 3f;
    public float resizeRate = 0.01f;
    public float baseHeight;

    float moveSpeed;
    float jumpSpeed;

    Vector2 moveDir;
    bool resizing = false;
    bool gettingbigger = false;

    void Start()
    {
        moveSpeed = moveSpeedSmall;
        jumpSpeed = jumpSpeedSmall;
    }
    public Size size;

    void Update()
    {
        if (gameObject.tag == "Player")
        {
            move(InputManager.HorizAxis);
            if (Math.Abs(rigidbody2D.velocity.y) <= 0.001f && InputManager.JumpDown) jump();
        }

        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;

        if (resizing)
        {
            if (gettingbigger) b.size = new Vector2(b.size.x + resizeRate, b.size.y + resizeRate);
            else b.size = new Vector2(b.size.x - resizeRate, b.size.y - resizeRate);
            baseHeight = transform.position.y - (b.size.y / 2);

            if (size == Size.LARGE && b.size.x >= boxSizeLarge)
            {
                resizing = false;
                b.size = new Vector2(boxSizeLarge, boxSizeLarge);
            }
            else if (size == Size.MEDIUM && gettingbigger && b.size.x >= boxSizeMedium)
            {
                resizing = false;
                b.size = new Vector2(boxSizeMedium, boxSizeMedium);
            }
            else if (size == Size.MEDIUM && !gettingbigger && b.size.x <= boxSizeMedium)
            {
                resizing = false;
                b.size = new Vector2(boxSizeMedium, boxSizeMedium);
            }
            else if (size == Size.SMALL && b.size.x <= boxSizeSmall)
            {
                resizing = false;
                b.size = new Vector2(boxSizeSmall, boxSizeSmall);
            }
        }

        baseHeight = transform.position.y - (b.size.y / 2) + 0.01f;
    }

    public void move(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed * moveSpeed, rigidbody2D.velocity.y);
    }

    public void jump()
    {
        rigidbody2D.velocity = new Vector2(0f, jumpSpeed);
    }

    public void changeSize(Size s)
    {
        if (s == Size.LARGE)
        {
            Debug.Log("Changing character to large.");
            moveSpeed = moveSpeedLarge;
            jumpSpeed = jumpSpeedLarge;
            // TODO: call animation
            resizing = true;
            gettingbigger = true;
        }
        else if (s == Size.MEDIUM)
        {
            Debug.Log("Changing character to medium.");
            moveSpeed = moveSpeedMedium;
            jumpSpeed = jumpSpeedMedium;
            // TODO: call animation
            resizing = true;
            if (size == Size.SMALL) gettingbigger = true;
            else gettingbigger = false;
        }
        else if (s == Size.SMALL)
        {
            Debug.Log("Changing character to small.");
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
        if (size == Size.LARGE) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.SMALL);
        else if (size == Size.SMALL) GetComponent<PlayerDeath>().die();
    }

    public void makeFatter()
    {
        if (size == Size.SMALL) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.LARGE);
        else if (size == Size.LARGE) GetComponent<PlayerDeath>().die();
    }
}
