using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {
	
	float angle;

	public float blinkOffset = 0;
	public float blinkRate = 0.5f;
	public Color color = Color.white;

	private Color transblack = new Color(0.45f,0,0,0.5f);

	LineRenderer lines;

	// Use this for initialization
	void Start () {

		Vector3 Null;
		transform.localRotation.ToAngleAxis(out angle, out Null);
		lines = GetComponent<LineRenderer>();
		lines.SetColors(color, transblack);

		enabled = false;
		lines.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.AngleAxis(-angle + 15.0f * Mathf.Sin (blinkOffset + blinkRate * Time.time), Vector3.forward);

		color.a = (0.5f + 0.5f * Mathf.Sin(blinkOffset + blinkRate * Time.time));

		if(lines) lines.SetColors(color, transblack);
	}

	void WhatIsLove() {
		enabled = true;
		lines.enabled = true;
	}

}
