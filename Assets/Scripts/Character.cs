﻿using UnityEngine;
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
    float boxSizeSmall = 0.9f;
    float boxSizeMedium = 1.9f;
    float boxSizeLarge = 2.9f;
    float resizeRate = 0.01f;
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
        //GameObject.Instantiate(checkpointTemplate, new Vector3(transform.position.x - 2, transform.position.y - 2, transform.position.z), Quaternion.identity);
    }
    public Size size;

    void Update()
    {
        if (gameObject.tag == "Player")
        {
            move(InputManager.HorizAxis);
            if (InputManager.JumpDown) jump();
            if (InputManager.RestartDown) SpawnScript.levelReset();
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
        if (rigidbody2D.velocity.y < jumpSpeed / 4) rigidbody2D.velocity += new Vector2(0f, jumpSpeed) / 4;
        else rigidbody2D.velocity += new Vector2(0f, jumpSpeed) / 20;
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

    public void forceSmall()
    {
        size = Size.SMALL;
        resizing = false;
        (gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D).size = new Vector2(boxSizeSmall, boxSizeSmall);
        moveSpeed = moveSpeedSmall;
        jumpSpeed = jumpSpeedSmall;
    }
}
