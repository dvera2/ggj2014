using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	public string IntroQuote;
	private const float textPrintRate = 0.05f;
	private float showLevelIntroTimer = 3.0f;
	private Texture2D blackTexture;
	private string currentIntroQuote;
	public GUIStyle quoteStyle = new GUIStyle();

	void Start() {
		showLevelIntroTimer = 8.0f;
		blackTexture = new Texture2D (1, 1);
		blackTexture.SetPixel (0, 0, Color.black);
		blackTexture.Apply ();
		currentIntroQuote = "";
		
		PrintQuote ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void PrintQuote()
	{
		StartCoroutine( WaitForPrint() );
	}
	
	IEnumerator WaitForPrint()
	{
		for (int i = 0; i < IntroQuote.Length; i++) 
		{
			currentIntroQuote += IntroQuote[i];
			yield return new WaitForSeconds (textPrintRate);
		}
	}
	
	void OnGUI()
	{
		if(currentIntroQuote == IntroQuote)
			showLevelIntroTimer -= Time.deltaTime;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackTexture);
		
		if (showLevelIntroTimer > 0.0f) {
						
						GUI.Label (new Rect (0, 0, Screen.width, Screen.height), currentIntroQuote, quoteStyle);
				} else {
			StateManager.nextLevel();
				}
	}	
}
