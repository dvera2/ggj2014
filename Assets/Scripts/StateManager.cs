using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
		
	private SpriteRenderer fadeSprite;

	private float fadeRate = 0.3f;

	public static StateManager Instance {
		get {
			if(!instance) {
				GameObject sm = new GameObject();
				sm.transform.parent = null;
				sm.name = "StateManager";

				instance = sm.AddComponent<StateManager>();
			}

			return instance;
		}
	}

	private SpriteRenderer FadeSprite {
		get {
			if(!fadeSprite) {
				fadeSprite = CreateTexture();
			}
			return fadeSprite;
		}
	}


	private static StateManager instance = null;

    protected StateManager() {}

    public static void changeLevel(string s)
    {
        Application.LoadLevel(s);
    }

	private SpriteRenderer CreateTexture() 
	{
		var blackTexture = new Texture2D (1, 1);
		blackTexture.SetPixel (0, 0, Color.black);
		blackTexture.Apply ();

		
		var fadeSprite = Sprite.Create(blackTexture, new Rect(0,0,1,1), 0.5f * Vector2.one);

		var blackness = new GameObject().AddComponent<SpriteRenderer>();
		blackness.sprite = fadeSprite;
		blackness.sortingLayerName = "Text";
		blackness.sortingOrder = 100;

		var cam = Camera.main.camera;

		var pos = cam.transform.position - cam.transform.forward;
		blackness.transform.position = pos;
		blackness.transform.localScale = new Vector3(10 * cam.pixelWidth, 10 * cam.pixelHeight, 1);
		return blackness;
	}

    public static void nextLevel()
    {
        string level = getCurrentLevel();
		if (level == "MainMenu") changeLevel("Level1Transition");
		else if (level == "Level1Transition") changeLevel("Level1");
        else if (level == "Level1") changeLevel("Level2Transition");
		else if (level == "Level2Transition") changeLevel("Level2");
		else if (level == "Level2") changeLevel("Level3Transition");
		else if (level == "Level3Transition") changeLevel("Level3");
		else if (level == "Level3") changeLevel("Level4Transition");
		else if (level == "Level4Transition") changeLevel("Level4");
        else if (level == "Level4") changeLevel("Ending");
		else if (level == "Ending") changeLevel("Credits");
    }

	public void fadeToNextLevel() {
		StartCoroutine(FadeOut());
	}

	public void fadeToScene(string sceneName) {
		StartCoroutine(FadeOut (sceneName));
	}

	IEnumerator FadeIn() {
		var sprite = FadeSprite;
		
		var a = 1.0f;
		var color = sprite.color;
		
		while(a > 0) {
			a -= fadeRate * Time.deltaTime;
			color = sprite.color;
			color.a = a;
			sprite.color = color;
			yield return new WaitForEndOfFrame();
		}
	}

	void Update() {
		var cam = Camera.main.camera;
		FadeSprite.transform.position = cam.transform.position + cam.transform.forward;
	}

	IEnumerator FadeOut() {
		var sprite = FadeSprite;

		var a = 0.0f;
		var volume = ResetVolume();
		var color = sprite.color;

		while(a < 1.0f) {
			a += fadeRate * Time.deltaTime;
			color = sprite.color;
			color.a = a;
			sprite.color = color;
			
			volume -= fadeRate * Time.deltaTime;
            SetVolume(volume);
			yield return new WaitForEndOfFrame();
		}
		
		StateManager.nextLevel();
	}

	IEnumerator FadeOut(string sceneName) {
		var sprite = FadeSprite;
		
		var a = 0.0f;
		var volume = ResetVolume();
		var color = sprite.color;
		
		while(a < 1.0f) {
			a += fadeRate * Time.deltaTime;
			color = sprite.color;
			color.a = a;
			sprite.color = color;
			volume -= fadeRate * Time.deltaTime;
			SetVolume(volume);
			yield return new WaitForEndOfFrame();
		}
		
		StateManager.changeLevel(sceneName);
	}
	


    public static string getCurrentLevel()
    {
        return Application.loadedLevelName;
    }

	void SetVolume(float volume) {
		var cam = Camera.main.camera;
		var music = cam.GetComponent<AudioSource>();
		if(music) {
			music.volume = volume;
		}
	}

	float ResetVolume() {
		var cam = Camera.main.camera;
		var music = cam.GetComponent<AudioSource>();
		if(music) {
			return music.volume;
        }
		return 0;
	}
}
