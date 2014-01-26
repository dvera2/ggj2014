using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour {

	void Start () {
        gameObject.renderer.enabled = false;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            StateManager.nextLevel();
        }
    }
}
