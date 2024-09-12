using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{   
    public bool isPaused = true;
    public float timerDurationInSecond = 0f;
    public TextMeshProUGUI timerUIElement;

    private float preTime = 0f;
    private float timeLeft = 0f;
    private float timePausedTotal = 0f;
    private float timePaused = 0f;

    public bool isTimerEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        isTimerEnding = false;
        UpdateUI();
        timerDurationInSecond+=0.5f;//Used to add a second to the timer for a proper render
        GameManager.startGameEvent.AddListener(StartTimer);
    }

    void FixedUpdate()
    {
        if (isPaused) //handled like this to not mess with the dt and anim
        {
            timePaused = (Time.fixedTime - preTime);
            return;
        }
        if(timePaused > 0f)
        {
            timePausedTotal += timePaused;
            timePaused = 0f;
        }

        preTime = Time.fixedTime;

        timeLeft = (timerDurationInSecond + timePausedTotal) - Time.fixedTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            Respond();
        }

        UpdateUI();
    }

    private void Respond()
    {
        Debug.Log("TIMER FINISHED");
        isTimerEnding = true;
    }

    private void UpdateUI()
    {
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        timerUIElement.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public void ResetTimer(float newTime)
    {
        preTime = 0;
        timeLeft = 0;
        timePausedTotal = 0;
        timePaused = 0;

        isPaused = false;
        timerDurationInSecond = newTime;

        Start();
    }

    public void StartTimer()
    {
        isPaused = false;
    }
}
