using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScene : MonoBehaviour
{

    private bool paused;

    public void onPause()
    {
      
         Time.timeScale = 0;
        gameObject.SetActive(true);



    }

    public void onResume()
    {



        Time.timeScale = 1;
        gameObject.SetActive(false);

    }

    public void returnToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("settingScene");
    }

    public void onRestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("_Scene_0");
    }
}
