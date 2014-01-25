using UnityEngine;
using System;

public class Character : MonoBehaviour {
    public float jumpSpeed = 5f;
    public float moveSpeed = 2.5f;

    Vector2 moveDir;
    bool shouldJump = false;

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
}
