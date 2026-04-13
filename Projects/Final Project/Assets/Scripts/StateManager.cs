using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    
    
    public enum Stage
    {
        City, Town, Nature
    }
    
    public struct Score
    {
        string name;
        int score;
    }

    public Stage currentStage;
    public string gameScene = "Prototype 3";
    public string mainMenuScene = "MainMenuScene";
    public string pauseMenuScene = "PauseMenuScene";
    public string scoreboardScene = "MainMenuScene";

    // Assets
    public List<Sprite> backgroundsList;
    public List<GameObject> obstaclesList;
    public List<AudioClip> tracksList;

    public List<Score> Scoreboard;


    public bool pause = false;
    public bool victory = false;

    [HideInInspector]public GameManager gameManagerScript; // Reference set inside GameManager when loaded


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
        
    }

    // Update is called once per frame
    void Update()
    {


        // State control if game manager is loaded
        if (gameManagerScript != null)
        {
            // Pause game if playing, quit if at menu
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                Debug.Log("Escape Pressed");
                PauseStateToggle();
            }

            if (gameManagerScript.stageComplete)
            {
                gameManagerScript.StageComplete();   
            }
            if (!victory && gameManagerScript.screenFader.fadeComplete)
            {
                LoadNextStage();
            }
            else if (victory && gameManagerScript.screenFader.fadeComplete)
            {
                // Player won the game no need to load next stage
            }
        }

    }

    void PauseStateToggle()
    {
        //can be paused.
        if (!gameManagerScript.gameOver)
        {
            pause = !pause; // toggles pause state

            if (pause)
            {
                SceneManager.LoadScene(pauseMenuScene, LoadSceneMode.Additive);
            } 
            if (!pause)
            {
                Time.timeScale = 1f;  // Resume all physics
                SceneManager.UnloadSceneAsync(pauseMenuScene);
            } 
        }

    }

    public void StartGame()
    {
        // load game scene
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
        // unload Main menu
        SceneManager.UnloadSceneAsync(mainMenuScene);
        
    }

    public void LoadNextStage()
    {
        // unload current game scene
        SceneManager.UnloadSceneAsync(gameScene);
        // load game scene with new stage
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameScene);

    }


 
}
