using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
 public class enemiesDetails {
    public Dropdown colour;
    public Dropdown pointsPerKill;
    public Material _coulour;
    public Enemy enemy_prefab;

}

public class enemiesPointsAndColour : MonoBehaviour
{
    public List <enemiesDetails> list = new List <enemiesDetails> ();
    public Color[] c = { Color.white, Color.blue, Color.red, Color.green, Color.yellow };
    public int[] points = { 5, 10, 15, 20, 25 };
    void Awake()
    {
        foreach(enemiesDetails e in list)
        {
            e.colour.onValueChanged.AddListener((int i) => { settingDetails(e, i); });
            e.pointsPerKill.onValueChanged.AddListener((int i) => { settingPoints(e, i); });
            settingDetails(e, 0);
            settingDetails(e, 0);

        }


    }

    public void settingDetails( enemiesDetails  o , int i)
    {
        o._coulour.color = c[i];
    }

    public void settingPoints(enemiesDetails o, int i)
    {
        o.enemy_prefab.score = points[i];
    }

}
