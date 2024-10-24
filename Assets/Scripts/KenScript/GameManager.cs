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
    [SerializeField] private GameObject _Anim;

    [SerializeField] private Button _PauseButton;
    [SerializeField] private GameObject _PauseMenu;

    [SerializeField] private Timer _Timer;
    [SerializeField] private GameObject _GameOverMenu;


    public static UnityEvent startGameEvent = new UnityEvent();
    public static UnityEvent startDitacticiel = new UnityEvent();

    public DialogueSystem dialogueSys;

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
        StartDialogue();
        _PauseButton.onClick.AddListener(OnPause);
        WinScreenPanel.onNextDayButton.AddListener(GenerateNewsPaper);
        //GenerateNewsPaper();
        startGameEvent.AddListener(GenerateNewsPaper);
        startGameEvent.AddListener(StartTimer);
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
            Debug.LogError("LevelManager n'est pas initialisť !");
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
    private void StartTimer()
    {
        levelManager.ResetDayTimer(levelManager.currentDayIndex);
    }

    private void StartDialogue()
    {
        dialogueSys.textToDisplay=LevelManager.instance.dayDialogue[LevelManager.instance.currentDayIndex];
    }


    private void EndOfDayTask()
    {
        if(levelManager.dayNewsPapers[levelManager.currentDayIndex].newsPapers.Count <= 0)
        {
            _Anim.SetActive(true);
            StartCoroutine(LaucheWinWhenAnimEnd());
        }
    }

    private IEnumerator  LaucheWinWhenAnimEnd()
    {
        while (!_Anim.GetComponent<Anim>().isEnd)
        {
            yield return null;
        }
        _Anim.SetActive(false);

        _WinScreenPanel.SetActive(true);
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
        //Time.timeScale = 0f;
    }

    private void ShowDitacticiel()
    {
        if(_Ditacticiel)
            _Ditacticiel.SetActive(true);
    }


    private void OnDestroy()
    {
        startGameEvent.RemoveAllListeners();
    }
}
