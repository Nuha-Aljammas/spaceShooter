using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
           
            PrefsHelper.EndHistory(0, PrefsHelper.currentGame);
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
        });
    }
}
