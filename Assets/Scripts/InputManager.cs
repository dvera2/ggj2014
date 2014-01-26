using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public static float HorizAxis;
    public static bool JumpDown;
    public static bool RestartDown;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        HorizAxis = Input.GetAxis("Horizontal");
        JumpDown = Input.GetButtonDown("Jump");
        RestartDown = Input.GetButtonDown("Restart");
        if (Input.GetButtonDown("ChangeLevel1")) StateManager.changeLevel("Level1");
        if (Input.GetButtonDown("ChangeLevel2")) StateManager.changeLevel("Level2");
    }
}
