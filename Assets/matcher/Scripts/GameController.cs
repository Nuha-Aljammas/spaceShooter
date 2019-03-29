using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{   // serializefileld is to see private in the inspector

    [SerializeField]
    private Sprite BgImage;

    [SerializeField]
    private Text scoretxt;

    [SerializeField]
    private Text timertxt;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip click, win, lose;

    public Sprite[] cards;
    public List<Sprite> cardgame = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;
    private int  gameGuesses, correctGuesses;
    private int firstGuessIndx, secGuessIndx;
    private string firstGuessCard, SecGuessCard;

    //when score reaches 0 you loose
    private int _score =1000;
    public int score { get {return _score; }
        set
        {
            _score = value;
            scoretxt.text = "score: " + value;
        }
     }

    private float Startime, _timer;



    public float timer
    {
        get { return _timer ; }
        set
        {
            _timer = value;
            timertxt.text = "Time: " + (int) value;
        }
    }

    void Start()
    {
        PrefsHelper.currentGame = GameType.Memory;
        Startime = Time.time;
        GetButtons();
        AddListeners();
        AddGameCards();
        shuffle(cardgame);
        
        //if you uncover all cards you win.. 
        //divided by 2, because of the matching pairs 
        gameGuesses = cardgame.Count / 2;
    }
    void Update()
    {
        timer = Time.time - Startime;

    }


    void GetButtons()
    {   
        //setting a tag to access the buttons
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Cards");

        for(int i =0; i< objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            //assigning the backface img
            btns[i].image.sprite = BgImage;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => pickcard());
        }
    }

    void pickcard()
    {
        audio.PlayOneShot(click);

        if (!firstGuess)
        {
            firstGuess = true;
            //the pram in parse would return the name of the button
            //which is a number between 0 to 20
            firstGuessIndx = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            btns[firstGuessIndx].enabled = false;

            //save the name to compare with second guess 
            firstGuessCard = cardgame[firstGuessIndx].name;
            btns[firstGuessIndx].image.sprite = cardgame[firstGuessIndx];

        }


        //same goes for second guess
        else if (!secondGuess)
        {
            btns[firstGuessIndx].enabled = true;
            secondGuess = true;
    
            secGuessIndx = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            SecGuessCard = cardgame[secGuessIndx].name;
            btns[secGuessIndx].image.sprite = cardgame[secGuessIndx];
            StartCoroutine(checkMatchedCards());


        }
        
       

    }

    IEnumerator checkMatchedCards()
    {   
        //wait for 1 sec
        yield return new WaitForSeconds(1f);

        if(firstGuessCard == SecGuessCard)
        { correctGuesses++;
            audio.PlayOneShot(win);
            //game finished checks for score =0, or all matched
            if (gameFinished()==false){
                yield return new WaitForSeconds(.5f); 


                //this makes the buttons unclickable
                btns[firstGuessIndx].interactable = false;
                btns[secGuessIndx].interactable = false;

                //make them unvisable
                btns[firstGuessIndx].image.color = new Color(0,0,0,0);
                btns[secGuessIndx].image.color = new Color(0, 0, 0, 0);

                

            }
        }
        else
        {
           
            if (gameFinished() == false)
            {
                audio.PlayOneShot(lose);
                yield return new WaitForSeconds(.5f);
              
                score  -=40;
                //play loosing sound , and turn the cards back
                btns[firstGuessIndx].image.sprite = BgImage;
                btns[secGuessIndx].image.sprite = BgImage;
            }
        }
        yield return new WaitForSeconds(.5f);

        //reset the chooing variables
        firstGuess = secondGuess = false;

    }

        bool gameFinished()
        {
            if(score <= 0)
            {
            PrefsHelper.EndHistory(score, GameType.Memory);
            //game over .. play the loosing scene
            winning.time = _timer;
            winning.score = score;
            SceneManager.LoadScene("loosingScene");
            return true;
            }

            else if(correctGuesses == gameGuesses){
            //you guessed all correct, play winning scene
            PrefsHelper.EndHistory(score, GameType.Memory);
            winning.time = _timer;
            winning.score = score;
            SceneManager.LoadScene("winningScene");


                
                btns[firstGuessIndx].interactable = false;
                btns[secGuessIndx].interactable = false;

                //make them unvisable
                btns[firstGuessIndx].image.color = new Color(0, 0, 0, 0);
                btns[secGuessIndx].image.color = new Color(0, 0, 0, 0);
          
            return true;
            }
        return false;
        }

    void AddGameCards()
    {
        int loop = btns.Count;
        int indx = 0;
        
        for (int i =0; i< loop; i++)
        {
            if(indx == loop / 2)
            {
                indx = 0;
            }
            cardgame.Add(cards[indx]);
            indx++;
        }
    }

    void shuffle(List<Sprite> l)
    {
        for (int i =0; i< l.Count; i++)
        {
            Sprite temp = l[i];
            int randomIndx = Random.Range(i, l.Count);
            l[i] = l[randomIndx];
            l[randomIndx] = temp;
        }

    }
}
