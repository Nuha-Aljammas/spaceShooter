using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevels : MonoBehaviour
{

    public List<levelScene> buttons = new List<levelScene>();
    public static List <levelScene> list2;
    public levels Lev;

    public void onBtnClick(int i)
    {
        Debug.Log(buttons[0].levelName);
        Lev.transform.SetAsLastSibling();
        Lev.l = buttons[i];
        if (i == 0)
        {
            Lev.restricting = null;
        }
        else Lev.restricting = buttons[i-1];

 
            if (i > 0 && buttons[i].maxEnemie > buttons[i - 1].maxEnemie)
            {
                buttons[i].maxEnemie = buttons[i - 1].maxEnemie + 1;
                Debug.Log("Error invalid number of selection");
            }
         
        
        Lev.show();


    }

    void Awake()
    {
        list2 = buttons;

    }
}
