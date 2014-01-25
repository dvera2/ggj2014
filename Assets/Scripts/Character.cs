using UnityEngine;
using System;

public class Character : MonoBehaviour {

    public enum Size {
        SMALL, MEDIUM, LARGE
    }

    public float jumpSpeed = 5f;
    public float moveSpeed = 2.5f;

    Vector2 moveDir;
    Size size;

    void Update()
    {
        if (gameObject.tag == "Player")
        {
            move(InputManager.HorizAxis);
            if (Math.Abs(rigidbody2D.velocity.y) <= 0.001f && InputManager.JumpDown) jump();
        }
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
        size = s;
        if (s == Size.LARGE)
        {
            Debug.Log("Changing character to large.");
        }
        else if (s == Size.MEDIUM)
        {
            Debug.Log("Changing character to medium.");
        }
        else if (s == Size.SMALL)
        {
            Debug.Log("Changing character to small.");
        }
        
    }

    public void makeThinner()
    {
        if (size == Size.LARGE) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.SMALL);
    }

    public void makeFatter()
    {
        if (size == Size.SMALL) changeSize(Size.MEDIUM);
        else if (size == Size.MEDIUM) changeSize(Size.LARGE);
    }
}
