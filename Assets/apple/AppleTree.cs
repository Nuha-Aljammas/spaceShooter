using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{ [Header ("Set in Inspector")]
	public GameObject applePrefab;
	public float Speed=1f;
	public float leftAndRightEdge=10f;
	public float chanceTochangeDirection=0.1f;
	public float secondsBetweenAppleDrops =1f;
    
	
    // Start is called before the first frame update
    void Start()
    {
        //dropping apples every sec
        Invoke("DropApple", 2f);
    }
    void DropApple()
    {
       GameObject apple = Instantiate <GameObject> (applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
       
    }


    // Update is called once per frame
    void Update()
    {   //basic movement
        Vector3 pos = transform.position;
        pos.x+= Speed * Time.deltaTime;
        transform.position = pos;

        //changing directions 
        if (pos.x < -leftAndRightEdge)
        {
            Speed = Mathf.Abs(Speed); //move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            Speed = -Mathf.Abs(Speed); //move left
        }

    }
    void FixedUpdate()
    {
        if (Random.value < chanceTochangeDirection)
        {
            Speed *= -1; //change direction
        }

    }
}
