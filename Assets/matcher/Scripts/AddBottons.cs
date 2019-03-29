using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddBottons : MonoBehaviour
{
    [SerializeField]
    private Transform gameField;

    [SerializeField]
    private GameObject btn;

    private static int cardCounter=8;

    void Awake()
    {
        
        for(int i =0; i < cardCounter; i++)
        {
            GameObject botton = Instantiate(btn);
            botton.name = "" + i;
            botton.transform.SetParent(gameField, false);
        }

    }

   public static void setcardCount(int a)
    {
        cardCounter = a;
    }
}
