using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public MoneySystem moneySystem;
    public GameObject BadEnding;
    public GameObject BetterEnding;

    public List<DayNewsPapers> dayNewsPapers = new List<DayNewsPapers>();
    public List<float> dayTimer = new List<float>();

    [TextArea(5, 5)] public string[] dialogueDay1;
    [TextArea(5, 5)] public string[] dialogueDay2;
    [TextArea(5, 5)] public string[] dialogueDay3;
    [TextArea(5, 5)] public string[] dialogueDay4;
    public List<string[]> dayDialogue = new List<string[]>();

    private List<DayNewsPapers> originalDayNewsPapers = new List<DayNewsPapers>();
    public int currentDayIndex;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Deux instances de Level Manager type ne peuvent exister");
        }
        instance = this;

        foreach (DayNewsPapers day in dayNewsPapers)
        {
            DayNewsPapers lDayCopy = new DayNewsPapers
            {
                newsPapers = new List<NewsPaper>(day.newsPapers) 
            };
            originalDayNewsPapers.Add(lDayCopy);
        }

        
    }
    private void Start()
    {
        dayDialogue.Add(dialogueDay1);
        dayDialogue.Add(dialogueDay2);
        dayDialogue.Add(dialogueDay3);
        dayDialogue.Add(dialogueDay4);
        WinScreenPanel.onNextDayButton.AddListener(ChangeToNextDay);
    }

    private void ChooseEnding()
    {
        if (moneySystem.saving >= moneySystem.moneyGoal)
        {
            BetterEnding.SetActive(true);
        }
        else 
        {
            BadEnding.SetActive(true);
        }
    }

    public void ChangeToNextDay()
    {
        currentDayIndex++;
        if (currentDayIndex == 4)
        {
            ChooseEnding();
        }
        else
        {
            ResetDayNewsPapers(currentDayIndex);
            ResetDayTimer(currentDayIndex);
        }
    }
    public void Changedays(int Days)
    {
        currentDayIndex = Days;
        ResetDayNewsPapers(Days);
        ResetDayTimer(Days);
    }

    public void ResetDayNewsPapers(int dayIndex)
    {
        if (dayIndex < 0 || dayIndex >= dayNewsPapers.Count)
        {
            Debug.LogError("Index de jour invalide pour la réinitialisation !");
            return;
        }
        
        dayNewsPapers[dayIndex].newsPapers = new List<NewsPaper>(originalDayNewsPapers[dayIndex].newsPapers);
        Debug.Log(dayNewsPapers[dayIndex].newsPapers);
    }

    public void ResetDayTimer(int dayIndex)
    {
        Timer.instance.ResetTimer(dayTimer[dayIndex]);
    }
  

    public void OnDisable()
    {
        WinScreenPanel.onNextDayButton.RemoveListener(ChangeToNextDay);
    }

}


[System.Serializable]
public class DayNewsPapers
{
    public List<NewsPaper> newsPapers; 
}
