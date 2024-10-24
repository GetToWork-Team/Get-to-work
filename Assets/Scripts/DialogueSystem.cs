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

    public bool finished = false;
    private string displayedText = "";
    private int position = 0;
    private int tablePosition = 0;
    private float preTime = 0f;

    [SerializeField] private Transition _Transition;

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
                if (textToDisplay[tablePosition].Length <= 0)
                {
                    finished = true;
                    return;
                }
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
                
                _Transition.FadeOut();
                
                if(LevelManager.instance.currentDayIndex == 0)
                   StartCoroutine(WaitDidact());
                
            }
        }

    }
    //--------------------------------------------------

    private IEnumerator WaitDidact()
    {
        yield return new WaitForSeconds(1.2f);
        GameManager.startDitacticiel?.Invoke();
        Array.Clear(textToDisplay, 0, textToDisplay.Length);
    }

    private void OnDestroy()
    {
        StopCoroutine(WaitDidact());
    }

    public void Reset(string[] newDialogue)
    {
        displayedText = "";
        position = 0;
        tablePosition = 0;
        preTime = 0f;
        Start();

        _Transition.Activate();
        _Transition.FadeIn();

        StartCoroutine(StartWhenReset(newDialogue));
    }

    public bool IsTransitionDark()
    {
        return _Transition._CanvasGroup.alpha >= 1f;
    }

    private IEnumerator StartWhenReset(string[] newDialogue)
    {
        while(!IsTransitionDark())
        {
            yield return null;
        }
        textToDisplay = newDialogue;
        finished = false;
    }
}
