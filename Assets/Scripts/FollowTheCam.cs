using UnityEngine;
using System.Collections;

public class FollowTheCam : MonoBehaviour {
	public Camera cam;

	private Vector3 origPos;

	// Use this for initialization
	void Start () {

		origPos = transform.position;

		if (!cam) {
			var obj = GameObject.FindGameObjectWithTag ("MainCamera");
			if(obj) cam = obj.GetComponent<Camera>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float z = transform.position.z;
		var pos = cam.transform.position;
		pos.z = z;
		transform.position = pos;
	}
}
