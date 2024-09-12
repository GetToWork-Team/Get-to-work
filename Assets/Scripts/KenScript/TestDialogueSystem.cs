using UnityEngine;
using TMPro;
using System.Collections;

public class TestDialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public float speedBetweenLetterInSecond = 0.1f;
    [TextArea(5, 5)] public string[] textToDisplay;

    [SerializeField] private bool finished = false;
    private string displayedText = "";
    private int position = 0;
    private int tablePosition = 0;

    void Start()
    {
        TMP.text = "";
    }

    // Coroutine to handle the text display
    private IEnumerator DisplayText()
    {
        finished = false;
        displayedText = "";
        TMP.text = "";
        position = 0;

        while (position < textToDisplay[tablePosition].Length)
        {
            displayedText += textToDisplay[tablePosition][position];
            TMP.text = displayedText;
            position++;

            yield return new WaitForSeconds(speedBetweenLetterInSecond);
        }

        finished = true;
    }

    //--------------------------------------------------
    // Handle Mouse Click
    //--------------------------------------------------
    private void OnMouseDown()
    {
        if (!finished)
        {
            StopAllCoroutines();  // Stop the current coroutine if not finished
            TMP.text = textToDisplay[tablePosition];
            finished = true;
        }
        else
        {
            if (tablePosition < (textToDisplay.Length - 1))
            {
                tablePosition++;
                StartCoroutine(DisplayText());
            }
            else
            {
                // END OF DIALOGUE;
            }
        }
    }

    // Public method to start the dialogue
    public void StartDialogue()
    {
        tablePosition = 0;
        StartCoroutine(DisplayText());
    }
}
