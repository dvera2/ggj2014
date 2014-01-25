using UnityEngine;
using System;

public class Character : MonoBehaviour {
    public float jumpSpeed = 5f;
    public float moveSpeed = 2.5f;

    Vector2 moveDir;
    bool shouldJump = false;

    public void move(float speed)
    {
        moveDir = new Vector2(speed * moveSpeed, rigidbody2D.velocity.y);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir;

        if (shouldJump)
        {
            if (Math.Abs(rigidbody2D.velocity.y) <= 0.001f)
                rigidbody2D.velocity += new Vector2(0f, jumpSpeed);

            shouldJump = false;
        }
    }

    public void jump()
    {
        shouldJump = true;
    }
}
