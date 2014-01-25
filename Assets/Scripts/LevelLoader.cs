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

	public bool generateSet = false;

	private const float tileScale = 0.99f;
	private Transform tileSet;

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

				int currentBoxColliderY = -1;

				foreach(XmlNode tile in tiles)
				{
					if(tile.Name == "tile")
					{
						float x = float.Parse( tile.Attributes["x"].Value );
						float y = maxTileY - float.Parse( tile.Attributes["y"].Value );
						int tileId = int.Parse(tile.Attributes["id"].Value);

						// Scale slightly to avoid gaps in tiles
						//x *= tileScale;
						//y *= tileScale;

						GameObject newTile = GameObject.Instantiate(Tile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
						newTile.transform.parent = tileSet.transform;

						if(tileId >= 0 && tileId < TileSprites.Length)
							newTile.GetComponent<SpriteRenderer>().sprite = TileSprites[tileId];
					}
				}
			}
		}

	}
}
