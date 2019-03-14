using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundScaling : MonoBehaviour
{

    public Image preview;
    public Sprite [] bgImgs;
    public Dropdown bgChosen;
    public Material bgMat;
    public Slider xslider;
    public Slider yslider;
    public float x = 1;
    public float y = 1;

    public void imagDroupMenu(int i)
    {
        preview.sprite = bgImgs[i];
        bgMat.mainTexture = bgImgs[i].texture;

    }

     void Awake()
    {
      bgChosen.onValueChanged.AddListener(imagDroupMenu);
        xslider.onValueChanged.AddListener(XScaling);
        yslider.onValueChanged.AddListener(YScaling);

    }

    public void XScaling( float f)
    {
        x = f;
        bgMat.SetTextureScale("_MainTex", new Vector2(x, y));
    }

    public void YScaling(float f)
    {
        x = f;
        bgMat.SetTextureScale("_MainTex", new Vector2(x, y));
    }

}
