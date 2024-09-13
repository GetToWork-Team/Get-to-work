using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Anim : MonoBehaviour
{

    public Sprite im1;
    public Sprite im2;
    public Sprite im3;

    private int pos = 0;

    public Image Image;

    public bool isEnd = false;

    private void OnEnable()
    {
        Image.sprite = im1;
        
    }
   
    private void OnMouseDown()
    {
        pos++;
        switch (pos)
        {
            case 0:
                break;
            case 1:
                Image.sprite = im2;
                break;
            case 2:
                Image.sprite = im3;
                break;
            default:
                isEnd = true;
                break;
        }
    }
    

}