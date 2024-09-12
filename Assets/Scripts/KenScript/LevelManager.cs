using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public MoneySystem moneySystem;
    public GameObject BadEnding;
    public GameObject BetterEnding;

    public List<DayNewsPapers> dayNewsPapers = new List<DayNewsPapers>();
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
        }
    }
    public void Changedays(int Days)
    {
        currentDayIndex = Days;
        ResetDayNewsPapers(Days);
    }

    public void ResetDayNewsPapers(int dayIndex)
    {
        if (dayIndex < 0 || dayIndex >= dayNewsPapers.Count)
        {
            Debug.LogError("Index de jour invalide pour la r�initialisation !");
            return;
        }
        
        dayNewsPapers[dayIndex].newsPapers = new List<NewsPaper>(originalDayNewsPapers[dayIndex].newsPapers);
        Debug.Log(dayNewsPapers[dayIndex].newsPapers);
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