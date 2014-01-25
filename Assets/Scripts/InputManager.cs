using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private Character character;

	// Use this for initialization
	void Start ()
    {
        character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        character.move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            character.jump();
	}
}
