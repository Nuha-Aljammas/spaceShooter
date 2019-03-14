using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance { get; private set; }

    public AudioSource bg;
    public AudioSource win;
    public AudioSource shoot;
    public AudioSource distroy;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    
    
}
