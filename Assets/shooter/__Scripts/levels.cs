using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levels : MonoBehaviour
{
    public levelScene l;
    public levelScene restricting;
    public InputField score, maxEnemi;
    public TextMeshProUGUI title;
    public Toggle[] togArr ;

    public void show()
    {
        //update the txt
        title.text = "Level: " + l.levelName;

        //update checkbox
        for(int i=0; i<5; i++)
        {
          
            if (restricting == null)
            {
                togArr[i].isOn = l.chosen[i];
            }

            else
            {
                togArr[i].enabled= !restricting.chosen[i];
                if (togArr[i].enabled == true) { togArr[i].isOn = l.chosen[i]; }
                else { togArr[i].isOn = restricting.chosen[i]; }
            }
        
        }

        

        //update the score
        score.text = l.score.ToString();

        //update max enemi 

        maxEnemi.text = l.maxEnemie.ToString();

    }

    void Awake()
    {
        
        score.onEndEdit.AddListener((string s) => l.score = System.Int32.Parse(score.text));
        maxEnemi.onEndEdit.AddListener((string s) => l.maxEnemie=System.Int32.Parse(maxEnemi.text));


        int i = 0;
        foreach (var toggle in togArr) {
           // Debug.Log("in awak, levels");
            int j = i;
            toggle.onValueChanged.AddListener((bool togValue) => { l.chosen[j] = togValue; });
            i++;
         } 
    }

    

}


