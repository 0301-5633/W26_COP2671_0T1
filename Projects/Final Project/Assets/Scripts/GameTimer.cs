using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeRemaining;
    public float stageTime = 30f;
    public int score;
    public bool stageCompleted;


    private StateManager stateManager;
    private GameManager gameManagerScript;

    private float timer;


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        stageCompleted = false;
        //TODO: Might want to handle stage time based on stage for increasing difficulty???
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(timer) > 0 && !gameManagerScript.gameOver && !stateManager.pause) // Decrement timer and update display
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer: " + timer);
            UpdateTimerDisplay(timer);
        }
        if (Mathf.FloorToInt(timer) <= 0 && !gameManagerScript.gameOver && !stateManager.pause) // if timer runs out stage is complete
        {
            // Stage Complete
            Debug.Log("Stage Complete");
            stageCompleted = true;
            score = Mathf.FloorToInt(timer);
        }
    }
    
    void ResetTimer()
    {
        timer = stageTime;
    }

    void UpdateTimerDisplay(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        string currentTime = seconds.ToString();

        timeRemaining.text = "Time Remaining: " + currentTime;
    }
}
