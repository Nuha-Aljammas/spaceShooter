using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    //this will be used when loading the play scene or going back to main menu
    public void startClick(string s)
    {
        SceneManager.LoadScene(s);

    }

    //if you click on config botton
    public void config()
    {
        //make other buttons disappear

    }

    public void exitClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#endif
    }
}
