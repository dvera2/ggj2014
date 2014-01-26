using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static float HorizAxis;
    public static bool JumpDown;
    public static bool RestartDown;
    public static float MaxJumpTime = .2f;
    private float jumpTimer = 0f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        HorizAxis = Input.GetAxis("Horizontal");
        if (Math.Abs(rigidbody2D.velocity.y) <= 0.001f && Input.GetButtonDown("Jump")) JumpDown = true;
        else if (Input.GetButtonUp("Jump") || jumpTimer >= MaxJumpTime)
        {
            JumpDown = false;
            jumpTimer = 0f;
        }
        if(JumpDown) jumpTimer += Time.deltaTime;
        RestartDown = Input.GetButtonDown("Restart");
        if (Input.GetButtonDown("ChangeLevel1")) StateManager.changeLevel("Level1");
        if (Input.GetButtonDown("ChangeLevel2")) StateManager.changeLevel("Level2");
    }
}
