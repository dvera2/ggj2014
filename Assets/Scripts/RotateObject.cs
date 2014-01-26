using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	public float rotationRate = 20.0f;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.forward, rotationRate * Time.deltaTime);
	}
}
