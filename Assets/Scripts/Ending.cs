using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(OntoCredits());
	}
	
	// Update is called once per frame
	IEnumerator OntoCredits () {
		yield return new WaitForSeconds(18.0f);
		StateManager.Instance.fadeToScene("Credits");
	}
}
