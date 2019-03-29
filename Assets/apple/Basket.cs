using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header ("Set Dynamically")]
    public Text scoreGT;


    public static int Score;
    // Start is called before the first frame update
    void Start()
    {
        //find a refrence to the scoreCounter GAME OBJECT
        GameObject scoreGo = GameObject.Find("ScoreCounter");
        scoreGT = scoreGo.GetComponent<Text>();
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //Get the screen position of the mouse from input 
        Vector3 mousePos2D = Input.mousePosition;

        //the camera z position set how far to push the mouse into 3d
        mousePos2D.z = -Camera.main.transform.position.z;

        //convert the point from 2d screen space into 3d game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //move the x position of this basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

        void OnCollisionEnter(Collision coll)
        {
            //find out what hit the basket
            GameObject collidedWith = coll.gameObject;
            if(collidedWith.tag == "Apple")
            {
                Destroy(collidedWith);
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();
            Score = score;

            //track the high score
            if(score > HighScore.score)
            {
                HighScore.score = score;
            }
            }

        }


}

