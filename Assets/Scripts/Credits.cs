using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {

	GUIText creditItem;

	// Use this for initialization
	void Start () {

		creditItem = GetComponent<GUIText> ();

		AddTitle ("CANDY CRASH");
		AddTabs (4);

		AddTitle ("ENGINEERING AND DESIGN");
		AddTabs(2);
		AddName ("Jeffrey Johnson");
		AddName ("Vishnu Narayana");
		AddName ("Jon Ross");
		AddName ("Patrick Traynor");
		AddName ("Dave Vera");

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

		AddTabs (2);
		AddTitle ("SPECIAL THANKS");
		AddTabs(2);
		AddName ("Wilfred Brimley");
	}

	void AddTitle(string title)
	{
		creditItem.text += title;
		creditItem.text += " ";
	}

	void AddName(string name)
	{
		creditItem.text += name;
		creditItem.text += "     ";
	}

	void AddTabs(int numTab)
	{
		for (int i = 0; i < numTab; i++)
		{
			creditItem.text += "    ";
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime * 0.1f);
	}
}
