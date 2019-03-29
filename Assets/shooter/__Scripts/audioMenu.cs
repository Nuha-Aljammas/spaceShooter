using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioMenu : MonoBehaviour
{
    public Slider s;
    public Dropdown choise;
    public AudioClip[] music;
    public AudioSource audi;
   
    

    // Start is called before the first frame update
    void Start()
    {
        choise.onValueChanged.AddListener(musicMenu);
        s.onValueChanged.AddListener(settingVolum);
    }

   

    public void settingVolum(float f)
    {
        audi.volume = f;
        audi.Play();
        

    }

    public void musicMenu(int i)
    {
        audi.clip = music[i];
        audi.Play();
    }

}
