using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private StateManager stateManager;

    public ScreenFader screenFader;
    private AudioSource gameManagerAudioSource;
    private SpriteRenderer stageBackground;
    private bool stageCleanedBool;
    private StageStartScript stageStart;
    
    private Coroutine timer;
    private Coroutine spawner;

    public AudioSource audioOutStageTrack;
    public SpawnManager spawnManagerScript;
    public GameTimer gameTimer;

    public AudioClip playerDeathMusic;
    public AudioClip stageCompleteMusic;
    public GameObject stageClearTitle;
    public GameObject victoryTitle;
    public GameObject gameOverTitle;
    public GameObject mainMenuButtonGO;
    
   



    public float gravityModifier;
    public float stageTime;
    public bool stageComplete = false;
    public bool gameOver = false;

    public int score;

    public bool IsGameStarted { get; set; } = false;


    //=========================
    // Functions
    //=========================

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        stateManager.gameManagerScript = this; // sets reference to GameManager once it has loaded in the state manager

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Time.timeScale = 0f;

        // Set variables to needed objects/components
        gameManagerAudioSource = GetComponent<AudioSource>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        stageBackground = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        audioOutStageTrack = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        screenFader = GameObject.Find("FaderImage").GetComponent<ScreenFader>();
        gameTimer = GameObject.Find("Timer").GetComponent<GameTimer>();

        stageStart = GameObject.Find("StageStart").GetComponent<StageStartScript>();

        
        // Set current stage and set up stage assets
        StageSetup();

        StartCoroutine(stageStart.CountdownRoutine());


    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStarted)
        {
            // check pause and handle needed game control
            Pause();

            // Game over
            CleanStageOnDeath();
        }
    }

    

    // Called to set up obstacles and background at the beginning of each stage
    void StageSetup()
    {
        // reset gravity with modifier each time game is loaded
        Physics.gravity = new Vector3(0,-9.81f,0);
        Physics.gravity *= gravityModifier;

        // build scene with appropriate assets
        stageBackground.sprite = stateManager.backgroundsList[(int)stateManager.currentStage];
        spawnManagerScript.obstaclePrefab = stateManager.obstaclesList[(int)stateManager.currentStage];
        audioOutStageTrack.clip = stateManager.tracksList[(int)stateManager.currentStage];

        
    }

    public void StartTimedEvents()
    {
        
        timer = StartCoroutine(gameTimer.StartTimer());
        spawner = StartCoroutine(spawnManagerScript.SpawnObstacle());
    }

    void CleanStageOnDeath()
    {
        if(gameOver && !stageCleanedBool)
        {
            StopCoroutine(timer);
            StopCoroutine(spawner);
            audioOutStageTrack.Stop();
            gameManagerAudioSource.PlayOneShot(playerDeathMusic, 1.0f);

            gameOverTitle.SetActive(true);
            
            mainMenuButtonGO.SetActive(true);

            screenFader.FadeToBlack();

            stageCleanedBool = true;
        }
    }

    public void StageComplete()
    {
        
        StopCoroutine(timer);
        StopCoroutine(spawner);
        
        stageComplete = false;

        audioOutStageTrack.Stop();


        if (stateManager.currentStage != StateManager.Stage.Nature)
        {
            gameManagerAudioSource.PlayOneShot(stageCompleteMusic, 0.3f);
            stateManager.currentStage++;
            stageClearTitle.SetActive(true);
            victoryTitle.SetActive(false);
        }
        else // this would mean that the stage just completed is the last stage
        {
            stateManager.victory = true;

            stageClearTitle.SetActive(false);
            victoryTitle.SetActive(true);
            mainMenuButtonGO.SetActive(true);

            screenFader.duration = 10f;
            gameManagerAudioSource.PlayOneShot(stateManager.tracksList[3]);
        }
        screenFader.FadeToBlack();
    }

    void Pause()
    {
        // Toggles music based on pause state
        if (stateManager.pause) { audioOutStageTrack.Pause(); }
        if (!stateManager.pause) { audioOutStageTrack.UnPause(); }

        // Toggles Time scale based on pause state
        if (stateManager.pause) { Time.timeScale = 0f; } // Pause all physics
        if (!stateManager.pause) { Time.timeScale = 1f; } // Pause all physics
     }

  

}
