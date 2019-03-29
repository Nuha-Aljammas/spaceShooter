using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingManue : MonoBehaviour
{
    public void Click(int b)
    {
        AddBottons.setcardCount(b);
        SceneManager.LoadScene("PlayScene");


    }


}
