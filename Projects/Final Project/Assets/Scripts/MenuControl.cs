using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public CanvasGroup mainMenuCanvas;
    public CanvasGroup scoreboardCanvas;

    // Directory information for storing scores
    // "MyDocuments/Get Outta Town/scores.txt"
    private static string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private string scoreSaveDir = Path.Combine(myDocuments, "Get Outta Town");
    private string scoresFileName = "scores.txt";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create directory for saving scores
        //Directory.CreateDirectory(scoreSaveDir);
        SetContext();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetContext(int caller = 1)
    {
        switch (caller)
        {
            case 1: // called from state manager for main menu
                mainMenuCanvas.alpha = 1f;
                scoreboardCanvas.alpha = .0f;
                break;
            case 2: // called from state manager for scoreboard after game victory
                mainMenuCanvas.alpha = .0f;
                scoreboardCanvas.alpha = 1f;
                break;
            case 3: // called from main menu for scoreboard
                mainMenuCanvas.alpha = .0f;
                scoreboardCanvas.alpha = 1f;
                break;
            case 4: // called from scoreboard for main menu
                mainMenuCanvas.alpha = 1f;
                scoreboardCanvas.alpha = .0f;
                break;
        }

        
    }

    void ScoreBoard()
    {

    }

    void readScores()
    {

    }

    void writeScores(List<StateManager.Score> scores)
    {

    }

    
}
