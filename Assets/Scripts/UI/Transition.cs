using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public CanvasGroup _CanvasGroup;

    public bool fadeIn = false;
    public bool fadeOut = false;

    public float fadeSpeed;

    private void Update()
    {
       
        if (fadeIn)
        {
            if(_CanvasGroup.alpha < 1)
            {
                _CanvasGroup.alpha += fadeSpeed * Time.deltaTime;
                if(_CanvasGroup.alpha >= 1 )
                {
                    fadeIn = false;
                    
                }
            }
        }

        if (fadeOut)
        {
            if (_CanvasGroup.alpha >= 0)
            {
                
                _CanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                if (_CanvasGroup.alpha == 0)
                {
                    fadeOut = false;
                    gameObject.SetActive(false);

                }
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
