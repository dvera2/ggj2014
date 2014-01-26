using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

[ExecuteInEditMode]
public class LevelLoader : MonoBehaviour {

	public TextAsset LevelXMLFile;
	public GameObject Tile;
	public Sprite[] TileSprites;
	public string IntroQuote;

	public bool generateSet = false;

	private const int tileSize = 24;
	private const float tileScale = 0.99f;
	private Transform tileSet;

	private const float textPrintRate = 0.05f;
	private float showLevelIntroTimer = 3.0f;
	private Texture2D blackTexture;
	private string currentIntroQuote;
	public GUIStyle quoteStyle = new GUIStyle();

	/// <summary>
	/// Attaches to or creates a collection transform to store generated tiles.
	/// </summary>
	/// <returns>The tile set transform.</returns>
	Transform TileSet() {
		Transform tileSet = transform.FindChild ("TileSet");
		if (!tileSet) {
			GameObject t = new GameObject("TileSet");
			t.transform.parent = transform;
			t.transform.localPosition = Vector3.zero;
			t.transform.localRotation = Quaternion.identity;
			t.transform.localScale = Vector3.one;
			
			tileSet = t.transform;
		}
		return tileSet;
	}

	void Start() {
		showLevelIntroTimer = 6.0f;
		blackTexture = new Texture2D (1, 1);
		blackTexture.SetPixel (0, 0, Color.black);
		blackTexture.Apply ();
		currentIntroQuote = "";

		quoteStyle.font.name = "courier";

		PrintQuote ();
	}

	void Update() {
	    if (generateSet) {
			generateSet = false;

			ClearChildren();
			Load ();
		}
	}

	/// <summary>
	/// Removes all child transforms of this object.
	/// </summary>
	/// <returns>The children.</returns>
	void ClearChildren() {
		Transform tileSet = TileSet ();
		Transform t;
		for (int i = tileSet.childCount - 1; i >= 0; i--) {
			t = tileSet.GetChild(i);

			if(Application.isEditor) {
				DestroyImmediate(t.gameObject);
			} else {
				Destroy(t.gameObject);
			}
		}
	}

	// Use this for initialization
	void Load () 
	{
        Debug.Log("Load method called");
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (LevelXMLFile.text);

		XmlNodeList levelList = xmlDoc.GetElementsByTagName ("level");

		int levelWidth = int.Parse( levelList [0].Attributes ["width"].Value );
		int levelHeight = int.Parse (levelList [0].Attributes ["height"].Value);
		int xTileCount = levelWidth / 24;
		int yTileCount = levelHeight / 24;


		Transform tileSet = TileSet ();

		foreach (XmlNode level in levelList) 
		{
			XmlNodeList tileSetList = level.ChildNodes;

			foreach(XmlNode tileList in tileSetList)
			{
				XmlNodeList tiles = tileList.ChildNodes;

				float maxTileY = 0;
				foreach(XmlNode tile in tiles)
				{
					if(tile.Name == "tile")
					{
						maxTileY = Mathf.Max(float.Parse(tile.Attributes["y"].Value), maxTileY);
					}
				}

				BoxCollider2D[,] tileArray = new BoxCollider2D[xTileCount, yTileCount];

				foreach(XmlNode tile in tiles)
				{
					if(tile.Name == "tile")
					{
						int x = int.Parse( tile.Attributes["x"].Value );
						int y = int.Parse( tile.Attributes["y"].Value );
						int tileId = int.Parse(tile.Attributes["id"].Value);

						// Scale slightly to avoid gaps in tiles
						//x *= tileScale;
						//y *= tileScale;

						GameObject newTile = GameObject.Instantiate(Tile, new Vector3((float)x, maxTileY - y, 0.0f), Quaternion.identity) as GameObject;
						newTile.transform.parent = tileSet.transform;

						if(tileId >= 0 && tileId < TileSprites.Length)
						{
							newTile.GetComponent<SpriteRenderer>().sprite = TileSprites[tileId];

							BoxCollider2D tileBox = newTile.GetComponent<BoxCollider2D>();
							
							if(x > 0)
							{
								BoxCollider2D leftBoxCollider = tileArray[x - 1, y];

								if(leftBoxCollider == null)
								{
									tileArray[x,y] = tileBox;
								}
								else
								{
									// Extend the current box to encompas, translate then scale
									//Vector3 avgPos = leftBoxCollider.transform.position;
									//avgPos.x = ((leftBoxCollider.transform.position.x - (leftBoxCollider.transform.localScale.x / 2))
									//              + (newTile.transform.position.x + (newTile.transform.localScale.x / 2))) / 2;
									//leftBoxCollider.transform.position = avgPos;
									leftBoxCollider.center = leftBoxCollider.center + new Vector2(0.5f, 0.0f);

									//Vector3 scale = leftBoxCollider.transform.localScale;
									//scale.x += tileBox.transform.localScale.x;
									//leftBoxCollider.transform.localScale = scale;
									leftBoxCollider.size = leftBoxCollider.size + new Vector2(1.0f, 0.0f);

									tileArray[x,y] = leftBoxCollider;
									
									if(Application.isEditor) {
										DestroyImmediate(tileBox);
									} else {
										Destroy(tileBox);
									}
								}
							}
							else
							{
								tileArray[x,y] = tileBox;
							}
						}
					}
				}
			}
		}
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

		if (showLevelIntroTimer > 0.0f) 
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), currentIntroQuote, quoteStyle);
		}
	}
}
