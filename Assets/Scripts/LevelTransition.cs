using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	public AudioSource  audioSource;
	public Color backColor = Color.black;

	public string IntroQuote;
	private const float textPrintRate = 0.05f;
	private float showLevelIntroTimer = 3.0f;
	private Texture2D blackTexture;
	private string currentIntroQuote;
	public GUIStyle quoteStyle = new GUIStyle();

	float alpha = 1.0f;

	bool shouldTransition = false;

	void Start() {
		showLevelIntroTimer = 6.0f;
		blackTexture = new Texture2D (1, 1);
		blackTexture.SetPixel (0, 0, backColor);
		blackTexture.Apply ();
		currentIntroQuote = "";
		
		PrintQuote ();
	}
	
	// Update is called once per frame
	void Update () {

		bool moveOn = (audioSource && !audioSource.isPlaying) || (!audioSource) || (audioSource && audioSource.volume == 0);

		if(moveOn && shouldTransition) {
			StateManager.nextLevel();
			
			shouldTransition = false;
		}

		if(shouldTransition) {
			FadeSound();
			StateManager.Instance.fadeToNextLevel();
		}
	}

	void FadeText() {
		
		alpha -= 1 * Time.deltaTime;

		var color = quoteStyle.font.material.color;		
		color.a = alpha;
		quoteStyle.font.material.color = color;
	}

	void FadeSound() {
		// decrease audio
		if(audioSource) 
			audioSource.volume -= 0.35f * Time.deltaTime;
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

		if(backColor.a > 0) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackTexture);
		}
		
		if (showLevelIntroTimer < 0.0f) {						
			shouldTransition = true;
		}
		
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), currentIntroQuote, quoteStyle);
	}	
}
