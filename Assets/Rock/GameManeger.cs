using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    enum elements { Rock=1, Paper, Scissor }

    private int playerChoose = -1;
    private int NuhaChoose = -1;
    
    private int nuhaScore = 0;
    private int playerScore = 0;
    private int roundCounter = 10;
    public Text NuhaScore, player_Score, roundText;

    public GameObject win;
    public GameObject loose;
    public GameObject draw;
    public GameObject buttons;

    public Sprite rock, paper, scissor;
    public Image nuhaImg, playerImg;

    // Update is called once per frame
    void Start()
    {
        PrefsHelper.currentGame = GameType.Rock;
       
    }


    public void checkWinner()
    {
        if (playerChoose == NuhaChoose)
        {
            //draw
        }

        else if (playerChoose==(int)elements.Paper && NuhaChoose==(int)elements.Rock)
        {
            playerScore++;
            //player wins
        }
        else if (playerChoose == (int)elements.Rock && NuhaChoose == (int)elements.Scissor)
        {
            //player wins
            playerScore++;
        }
        else if (playerChoose == (int)elements.Scissor && NuhaChoose == (int)elements.Paper)
        {
            //player wins
            playerScore++;
        }

        else
        {
            //Nuha wins
            nuhaScore++;
        }


    }



    //ask the player to click a button
    //then end the turn and move to NuhaChoose;
    public void PlayerChoose(int choose)
    {
        
        roundCounter--;
        playerChoose = choose;
        updateImg(playerImg, (elements)playerChoose);
        


        myChoice();
        updateImg(nuhaImg, (elements)NuhaChoose);


        checkWinner();
        updateScore();
        

        if(roundCounter == 0)
        {
            PrefsHelper.EndHistory(playerScore, GameType.Rock);
            buttons.SetActive(false);
            if(playerScore > nuhaScore)
            {
                win.SetActive(true);
            }
            else if(nuhaScore > playerScore)
            {
                loose.SetActive(true);
            }
            else
            {
                draw.SetActive(true);
            }
            
        }

    }

    public void myChoice()
    {
        NuhaChoose = Random.Range(1, 4);
    }

    public void updateScore()
    {
        NuhaScore.text = "Nuha'S Score: " + nuhaScore;
        player_Score.text = "Your Score: " + playerScore;
        roundText.text = "Round: " + roundCounter;
        

        
    }

    private void updateImg(Image i, elements e)
    {
        switch (e)
        {
            case elements.Paper:
                i.sprite = paper;
                break;

            case elements.Rock:
                i.sprite = rock;
                break;

            case elements.Scissor:
                i.sprite = scissor;
                break;
        }

        i.gameObject.SetActive(true);

    }

    public void reloadGame()
    {
        SceneManager.LoadScene("rock");
    }
}
