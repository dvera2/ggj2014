using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour {

	private bool suckIn = false;

	void Start () {
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
			StateManager.Instance.fadeToNextLevel();

			if(collider) GameObject.Destroy(collider);

			var player = GameObject.FindGameObjectWithTag("Player");
			if(player) {
				player.transform.parent = transform;      
				player.GetComponent<Character>().enabled = false;
				player.rigidbody2D.isKinematic = true;
				player.collider2D.enabled = false;
				suckIn = true;
			}
		}
    }

	void Update() {
		if(suckIn)
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if(player) { 
				var toCenter = player.transform.position - transform.position;
				var pos = player.transform.position;
				pos -= (0.5f * Time.deltaTime * toCenter); 
				player.transform.position = pos;
			}
		}
	}
}
