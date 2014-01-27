using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

    float count = -60;
    float fadetime = 900;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count >= 0) gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (count / fadetime));
        if (count > 900) count = 900;
	}
}
