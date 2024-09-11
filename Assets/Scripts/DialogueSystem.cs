using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public float speedBetweenletterInSecond = 0.1f;
    [TextArea(5, 5)]
    public string textToDisplay = "";

    private bool finished = false;
    private string displayedText = "";
    private int position = 0;
    private float preTime = 0f;

    void Start()
    {
        TMP.text = "";
        preTime = speedBetweenletterInSecond;
    }

    void FixedUpdate()
    {
        if (!finished)
        {
            if (preTime + speedBetweenletterInSecond <= Time.fixedTime)
            {
                displayedText += textToDisplay[position];
                position++;
                TMP.text = displayedText;
                if (position == textToDisplay.Length)
                    finished = true;
            }
        }
    }
}
