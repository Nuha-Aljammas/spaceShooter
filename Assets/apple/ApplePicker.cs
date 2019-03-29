using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;// the space between baskets
    public List<GameObject> basketList;

    // Start is called before the first frame update
    void Start()
    {
        PrefsHelper.currentGame = GameType.Apple;
        basketList = new List<GameObject>();
        for(int i =0; i<numBaskets; i++)
        {
           GameObject tBasketGo = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGo.transform.position = pos;
            basketList.Add(tBasketGo);
        }
    }

    public void AppleDestroyed()
    {
        //Destroy all falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray){
            Destroy(tGO);
        }
        //destroy one of the baskets
        //get the index of the last basket in the list
        int basketIndex = basketList.Count - 1;
        GameObject tBasketGO = basketList[basketIndex];
        //remove the basket from the list then destroy that basket object
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        //if there are no baskets left, restart the game
        if(basketList.Count == 0)
        {
            PrefsHelper.EndHistory(Basket.Score, GameType.Apple);
            SceneManager.LoadScene("_Scene_a");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
