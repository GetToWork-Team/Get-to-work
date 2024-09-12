using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private LevelManager levelManager => LevelManager.instance;
    [SerializeField] private Transform NewsPaperContainer;
    [SerializeField] private Transform NewsPaperSpawnPosition;
    private NewsPaper _CurrentNewsPaper;
    private NewsPaper _SelectedNewsPaper;

    [SerializeField] private GameObject _WinScreenPanel;

    [SerializeField] private Button _PauseButton;
    [SerializeField] private GameObject _PauseMenu;

    [SerializeField] private Timer _Timer;
    [SerializeField] private GameObject _GameOverMenu;


    public static UnityEvent startGameEvent = new UnityEvent();
    public static UnityEvent startDitacticiel = new UnityEvent();

    [SerializeField] private GameObject _Ditacticiel;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Deux instance de GameManager ne peuvent esister");
            Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        _PauseButton.onClick.AddListener(OnPause);
        WinScreenPanel.onNextDayButton.AddListener(GenerateNewsPaper);
        //GenerateNewsPaper();
        startGameEvent.AddListener(GenerateNewsPaper);
        startDitacticiel.AddListener(ShowDitacticiel);
    }

    private void Update()
    {
        EndOfDayTask();
        TrashTest();
        CheckGameOver();

        Debug.Log(_Timer.isTimerEnding);
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
        _SelectedNewsPaper = levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers[lRandom];
        _CurrentNewsPaper = Instantiate(_SelectedNewsPaper, NewsPaperSpawnPosition.position, Quaternion.identity,NewsPaperContainer);
        Debug.Log(_CurrentNewsPaper);
    }

    private void EndOfDayTask()
    {
        if(levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Count <= 0)
        {
            _WinScreenPanel.SetActive(true);
        }
    }

    public void DestoyNewsPaper()
    {
        levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Remove(_SelectedNewsPaper);
        _SelectedNewsPaper = null;
        Destroy(_CurrentNewsPaper.gameObject);
        GenerateNewsPaper();
        
    }

    private void TrashTest()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Remove(_SelectedNewsPaper);
            _SelectedNewsPaper = null;
            Destroy(_CurrentNewsPaper.gameObject);
            GenerateNewsPaper();
        }
    }

    private void CheckGameOver()
    {
        if (_Timer.isTimerEnding)
        {
            _GameOverMenu.SetActive(true);
            _Timer.isTimerEnding = false;
        }
    }

    private void OnPause()
    {
        _PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowDitacticiel()
    {
        _Ditacticiel.SetActive(true);
    }


    private void OnDestroy()
    {
        startGameEvent.RemoveAllListeners();
    }
}
