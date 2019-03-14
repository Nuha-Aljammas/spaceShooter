using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevels : MonoBehaviour
{

    public List<levelScene> buttons = new List<levelScene>();
    public levels Lev;

    public void onBtnClick(int i)
    {
        Debug.Log(buttons[0].levelName);
        Lev.transform.SetAsLastSibling();
        Lev.l = buttons[i];
        Lev.show();


    }
}
