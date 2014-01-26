using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    protected StateManager() {}
    
    static string currentlevel = "Level1";

    public static void changeLevel(string s)
    {
        Application.LoadLevel(s);
        currentlevel = s;
    }

    public static string getCurrentLevel()
    {
        return currentlevel;
    }
}
