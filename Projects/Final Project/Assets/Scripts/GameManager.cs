using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private StateManager stateManager;
    
    private AudioSource gameManagerAudioSource;
    private SpawnManager spawnManagerScript;
    private SpriteRenderer stageBackground;
    private AudioSource audioOutStageTrack;
    private GameTimer gameTimerScript;
    private bool stageCompleteBool;
    private bool stageCleanedBool;


    public bool gameOver = false;
    public List<Sprite> backgroundsList;
    public List<GameObject> obstaclesList;
    public List<AudioClip> tracksList;
    public AudioClip playerDeathMusic;
    public AudioClip stageCompleteMusic;


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set variables to needed objects/components
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        stateManager.gameManagerScript = this; // sets reference to GameManager once it has loaded in the state manager

        gameManagerAudioSource = GetComponent<AudioSource>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        stageBackground = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        audioOutStageTrack = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameTimerScript = GameObject.Find("Timer").GetComponent<GameTimer>();

        // Set current stage and set up stage assets
        //currentStage = Stage.City;
        StageSetup(stateManager.currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        CleanStageOnDeath();

        // Toggles music based on pause state
        if (stateManager.pause) {audioOutStageTrack.Pause(); }
        if (!stateManager.pause) {audioOutStageTrack.UnPause(); }
    }

    //TODO: GameManager, Coroutine, Stage timer milestone
    // - Stage timer (implemented, Might want to clean up, and handle calls from Here)
    // - End of stage Logic (increment currentStage, call stage setup, start next stage)
    //

    // Called to set up obstacles and background at the beginning of each stage
    void StageSetup(StateManager.Stage currStage)
    {
        stageBackground.sprite = backgroundsList[(int)currStage];
        spawnManagerScript.obstaclePrefab = obstaclesList[(int)currStage];
        audioOutStageTrack.clip = tracksList[(int)currStage];
        audioOutStageTrack.Play();
    }

    void CleanStageOnDeath ()
    {
        if(gameOver && !stageCleanedBool)
        {
            // Timer stops as a result of gameOver bool
            audioOutStageTrack.Stop();
            gameManagerAudioSource.PlayOneShot(playerDeathMusic, 1.0f);

            stageCleanedBool = true;
        }
    }

    void StageComplete ()
    {
        gameManagerAudioSource.PlayOneShot(stageCompleteMusic, 1.0f);
        stateManager.currentStage++;
    }
}
