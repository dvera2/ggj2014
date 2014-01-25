using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class LevelLoader : MonoBehaviour {

	public TextAsset LevelXMLFile;
	public GameObject Tile;
	public Sprite[] TileSprites;

	private const float tileScale = 0.24f;

	// Use this for initialization
	void Start () 
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (LevelXMLFile.text);

		XmlNodeList levelList = xmlDoc.GetElementsByTagName ("level");

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

				foreach(XmlNode tile in tiles)
				{
					if(tile.Name == "tile")
					{
						float x = float.Parse( tile.Attributes["x"].Value );
						float y = maxTileY - float.Parse( tile.Attributes["y"].Value );
						int tileId = int.Parse(tile.Attributes["id"].Value);

						x *= tileScale;
						y *= tileScale;

						GameObject newTile = GameObject.Instantiate(Tile, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;

						if(tileId >= 0 && tileId < TileSprites.Length)
							newTile.GetComponent<SpriteRenderer>().sprite = TileSprites[tileId];
					}
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
