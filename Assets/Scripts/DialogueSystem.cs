using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public float speedBetweenLetterInSecond = 0.1f;
    [TextArea(5, 5)] public string[] textToDisplay;

    [SerializeField] private bool finished = false;
    private string displayedText = "";
    private int position = 0;
    private int tablePosition = 0;
    private float preTime = 0f;

    void Start()
    {
        TMP.text = "";
        preTime = speedBetweenLetterInSecond;
    }

    void FixedUpdate()
    {
        if (!finished)
        {
            if (preTime + speedBetweenLetterInSecond <= Time.fixedTime)
            {
                //PLAY SOND HERE
                SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_VoiceText, new Vector2(0, 0));

                preTime = Time.fixedTime;
                displayedText += textToDisplay[tablePosition][position];
                position++;
                TMP.text = displayedText;
                if (position == textToDisplay[tablePosition].Length)
                    finished = true;
            }
        }
    }

    //--------------------------------------------------
    // Handle Mouse Click
    //--------------------------------------------------
    private void OnMouseDown()
    {
        if (!finished)
        {
            finished = true;
            TMP.text = textToDisplay[tablePosition];
        }
        else
        {
            if (tablePosition < (textToDisplay.Length - 1))
            {
                finished = false;
                preTime = Time.fixedTime + speedBetweenLetterInSecond;
                displayedText = "";
                TMP.text = "";
                position = 0;
                tablePosition++;
            }
            else
            {
                //END OF DIALOGUE;
                GameManager.startDitacticiel?.Invoke();
                gameObject.SetActive(false);
                Array.Clear(textToDisplay, 0, textToDisplay.Length);
            }
        }

    }
    //--------------------------------------------------
}
