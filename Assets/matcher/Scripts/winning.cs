using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winning : MonoBehaviour
{
    [SerializeField]
    private Text scoretxt;

    [SerializeField]
    private Text timertxt;

    public static int score;
    public static float time;



    void Start()
    {
        scoretxt.text = "score: " + score;
        timertxt.text = "Time: " + (int)time;
    }



}
