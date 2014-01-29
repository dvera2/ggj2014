using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {

	public GUIText[] creditItems;
	public float danceDelay = 10.0f;
	public float scrollSpeed = 0.1f;

	// Use this for initialization
	void Start () {

		StartCoroutine(Dance());

		AddTitle ("CANDY CRASH");
		AddTabs (4);

		AddTitle ("ENGINEERING AND DESIGN");
		AddTabs(2);
		AddName ("Jeffrey Johnson");
		AddName ("Vishnu Narayana");
		AddName ("Jon Ross");
		AddName ("Patrick Traynor");
		AddName ("Dave Vera");
		AddName ("Subomi Awokoya");

		AddTabs(2);
		AddTabs(2);
		AddTitle ("ART AND DESIGN");
		AddTabs(2);
		AddName ("Jeff Berry");
		AddName ("Keith McDade");
		AddName ("Devlyn JD");

		AddTabs(2);
		AddTitle ("MUSIC AND ANIMATION");
		AddTabs(2);
		AddName("Dave Vera");
		AddName ("Boss Theme aka \"Cake\" by");
		AddName ("Dmitry Andreyev");

		AddTabs (2);
		AddTitle ("SPECIAL THANKS");
		AddTabs(2);
		AddName ("Wilfred Brimley");

		
		AddTabs (4);
		AddTitle ("THANKS FOR PLAYING!");

        StartCoroutine(ToMainMenu());
	}

	void AddTitle(string title)
	{
		creditItems[0].text += title;
		creditItems[0].text += "\n";

		
		creditItems[1].text += title;
		creditItems[1].text += "\n";
	}

	void AddName(string name)
	{
		creditItems[0].text += name;
		creditItems[0].text += "     \n";

		creditItems[1].text += name;
		creditItems[1].text += "     \n";
	}

	void AddTabs(int numTab)
	{
		for (int i = 0; i < numTab; i++)
		{
			creditItems[0].text += "\n"; //"    ";
			creditItems[1].text += "\n"; // "    ";
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
	}

	IEnumerator Dance()
	{
		yield return new WaitForSeconds(danceDelay);
		BroadcastMessage("WhatIsLove", SendMessageOptions.DontRequireReceiver);
    }
    
    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(80.0f);
		StateManager.Instance.fadeToScene("MainMenu");
    }
}
