using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public enum Stage
    {
        City, Town, Nature
    }

    public string gameScene = "Prototype 3";
    public string mainMenuScene = "MainMenuScene";
    public string pauseMenuScene = "PauseMenuScene";
    public Stage currentStage;

    public bool pause = false;

    [HideInInspector]public GameManager gameManagerScript; // Reference set inside GameManager when loaded


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Currently loads directly into game

        // TODO:
        // - Main Menu Scene load first
        // - Will need to close Main Menu for Game to appear
        // - ?? Maybe Load game scene in background disabled ?? for smoother switch
        // - Since Scene loading is additive, Pause menu will open on top of game scene
        // - 

        //SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause game if playing, quit if at menu
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("Escape Pressed");
            PauseStateToggle();
        }
    }

    void PauseStateToggle()
    {
        // verify the game is loaded and can be paused.
        if (gameManagerScript != null && !gameManagerScript.gameOver )
        {
            pause = !pause; // toggles pause state

            if (pause)
            {
                Time.timeScale = 0f; // Pause all physics
                SceneManager.LoadScene(pauseMenuScene, LoadSceneMode.Additive);
            } 
            if (!pause)
            {
                Time.timeScale = 1f;  // Resume all physics
                SceneManager.UnloadSceneAsync(pauseMenuScene);
            }

            
        }
        else // if gameManager is null then escape closes the game. This should only happen from main menu
        {
            Application.Quit();
        }
    }
}
