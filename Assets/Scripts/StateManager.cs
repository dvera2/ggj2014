using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    protected StateManager() {}

    public static void changeLevel(string s)
    {
        Application.LoadLevel(s);
    }

    public static void nextLevel()
    {
        string level = getCurrentLevel();
        if (level == "MainMenu") changeLevel("Level1");
        else if (level == "Level1") changeLevel("Level2");
        else if (level == "Level2") changeLevel("Level3");
        else if (level == "Level3") changeLevel("Level4");
        //else if (level == "Level4") changeLevel("Ending");
    }

    public static string getCurrentLevel()
    {
        return Application.loadedLevelName;
    }
}
