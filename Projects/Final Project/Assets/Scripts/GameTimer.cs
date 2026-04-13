using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private StateManager stateManager;
    private GameManager gameManagerScript;

    private float timer;


    public TextMeshProUGUI timeRemaining;

    


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        //TODO: Might want to handle stage time based on stage for increasing difficulty???
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartTimer()
    {
        while (Mathf.FloorToInt(timer) > 0)
        {
            if (!gameManagerScript.gameOver && !stateManager.pause) // Decrement timer and update display
            {
                timer -= Time.deltaTime;
                //Debug.Log("Timer: " + timer);
                UpdateTimerDisplay(timer);
            }
            if (Mathf.FloorToInt(timer) <= 0 && !gameManagerScript.gameOver && !stateManager.pause) // if timer runs out stage is complete
            {
                // Stage Complete
                gameManagerScript.stageComplete = true;
                gameManagerScript.score = Mathf.FloorToInt(timer);
            }
            yield return null;
        }
    }
    
    void ResetTimer()
    {
        timer = gameManagerScript.stageTime;
    }

    void UpdateTimerDisplay(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        string currentTime = seconds.ToString();

        timeRemaining.text = "Time Remaining: " + currentTime;
    }
}
