using UnityEngine;
using System.Collections;

public class IgnoreCameraOrth : MonoBehaviour {

	public float normalOrtho = 5f;

	public Camera cam;
	private Vector3 scale;

	// Use this for initialization
	void Start () {
		if(!cam) cam = Camera.main.camera;

		scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(!cam)
			return;

		float orth = cam.orthographicSize;
		float d = orth / normalOrtho;
		var s = d * scale;
		transform.localScale = s;
	}
}
