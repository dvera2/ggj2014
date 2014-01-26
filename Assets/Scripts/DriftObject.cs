using UnityEngine;
using System.Collections;

public class DriftObject : MonoBehaviour {

	public float driftRate = 1.0f;
	public float pixelsToUnits = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float w = Camera.main.pixelWidth / pixelsToUnits;
		float x = Camera.main.transform.position.x + 2 * w;

		var pos = transform.position;

		if (pos.x > x) {
			pos.x = Camera.main.transform.position.x - (2 * w);
		}

		pos.x += driftRate * Time.deltaTime;
		transform.position = pos;
	}
}
