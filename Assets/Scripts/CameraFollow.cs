using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;

	float currVelocity;
	float vel, velY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		var pos = transform.position;
		pos.x = Mathf.SmoothDamp(pos.x, target.position.x, ref vel, smoothTime);
		pos.y = Mathf.SmoothDamp(pos.y, target.position.y, ref velY, smoothTime);
		Debug.Log(string.Format("{0}, {1}", pos.x, pos.y));
		transform.position = pos;

		Debug.Log (string.Format ("{0}, {1}", transform.position.x, transform.position.y));
	}
}
