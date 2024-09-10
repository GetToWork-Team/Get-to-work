using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager => LevelManager.instance;
    [SerializeField] private Transform NewsPaperContainer;
    [SerializeField] private Transform NewsPaperSpawnPosition;
    private NewsPaper currentNewsPaper;

    [SerializeField] private GameObject _WinScreenPanel;


    private void Start()
    {
        GenerateNewsPaper();
    }

    private void Update()
    {
        TrashTest();
        EndOfDayTask();
    }

    public void GenerateNewsPaper()
    {
        if (levelManager == null)
        {
            Debug.LogError("LevelManager n'est pas initialisé !");
            return;
        }

        
        if (levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Count == 0)
        {
            Debug.LogWarning("Aucun journal disponible pour le jour actuel !");
            return;
        }

        int lRandom = Random.Range(0, levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Count);
        NewsPaper lSelectedNewsPaper = levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers[lRandom];
        currentNewsPaper = Instantiate(lSelectedNewsPaper, NewsPaperSpawnPosition.position, Quaternion.identity,NewsPaperContainer);
        levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Remove(lSelectedNewsPaper);
    }

    private void EndOfDayTask()
    {
        if(levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Count <= 0)
        {
            _WinScreenPanel.SetActive(true);
        }
    }

    public void TrashTest()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(currentNewsPaper.gameObject);
            GenerateNewsPaper();
        }
    }

    
}
