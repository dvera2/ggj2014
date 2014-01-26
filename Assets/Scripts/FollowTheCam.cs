using UnityEngine;
using System.Collections;

public class FollowTheCam : MonoBehaviour {
	public Camera cam;

	// Use this for initialization
	void Start () {
		if (!cam) {
			var obj = GameObject.FindGameObjectWithTag ("MainCamera");
			if(obj) cam = obj.GetComponent<Camera>();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float z = transform.position.z;
		var pos = cam.transform.position;
		pos.z = z;
		transform.position = pos;
	}
}
