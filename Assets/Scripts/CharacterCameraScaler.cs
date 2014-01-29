using UnityEngine;
using System.Collections;


public class CharacterCameraScaler : MonoBehaviour {

	public float camSizeSmall = 3.5f;
	public float camSizeMedium = 5;
	public float camSizeLarge = 7;

	public float resizeTime = 1f;

	private Camera cam;
	private Character character;
	private float vel = 0;

	// Use this for initialization
	void Start () {
		character = GetComponent<Character>();
		cam = Camera.main.camera;

		Camera.main.camera.orthographicSize = GetSize();
	}
	
	// Update is called once per frame
	void Update () {

		float target = GetSize ();
		if(cam.orthographicSize != target) {
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, target, ref vel, resizeTime);
		}
	}

	float GetSize() {
		switch(character.size) {
		case Character.Size.SMALL:
			return camSizeSmall;

		case Character.Size.MEDIUM:
			return camSizeMedium;

		case Character.Size.LARGE:
			return camSizeLarge;
		}

		return camSizeMedium;
	}
}
