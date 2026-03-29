using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Stage
    {
        City, Town, Nature
    }
    private AudioSource gameManagerAudioSource;
    private SpawnManager spawnManagerScript;
    private SpriteRenderer stageBackground;
    private AudioSource audioOutStageTrack;
    private PlayerController playerControllerScript;
    private bool stageCompleteBool;
    private bool stageCleanedBool;
    public List<Sprite> backgroundsList;
    public List<GameObject> obstaclesList;
    public List<AudioClip> tracksList;
    public AudioClip playerDeathMusic;
    public AudioClip stageCompleteMusic;
    public Stage currentStage;
    
    


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set variables to needed objects/components
        gameManagerAudioSource = GetComponent<AudioSource>();
        stageBackground = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioOutStageTrack = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Set current stage and set up stage assets
        //currentStage = Stage.City;
        StageSetup(currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        CleanStageOnDeath();
    }

    //TODO: GameManager, Coroutine, Stage timer milestone
    // - Stage timer
    // - End of stage Logic (increment currentStage, call stage setup, start next stage)
    //

    // Called to set up obstacles and background at the beginning of each stage
    void StageSetup(Stage currStage)
    {
        stageBackground.sprite = backgroundsList[(int)currStage];
        spawnManagerScript.obstaclePrefab = obstaclesList[(int)currStage];
        audioOutStageTrack.clip = tracksList[(int)currStage];
        audioOutStageTrack.Play();

        // TODO:
        // - Stage timer will be started from here, so that when the scene is recalled, the new timer will 
        // start with fresh timer, pulling correct assets based on current stage counter
        // - 
    }

    void CleanStageOnDeath ()
    {
        if(playerControllerScript.gameOver && !stageCleanedBool)
        {
            stageCleanedBool = true;

            // TODO:
            // - Stop timers
            audioOutStageTrack.Stop();
            gameManagerAudioSource.PlayOneShot(playerDeathMusic, 1.0f);
            
            // TODO:
            // Here handle scene switching to show death title screen
            // Scene should give user input options for try again or exit to menu
            // Double check understanding of scene loading and closing.
        }
    }

    // TODO:
    // Stage timers to be implemented later
    // Will be called when stage timer finished if player survived the whole time
    // Increments stage counter
    // Then recalls the scene, which is set up using the stage counter.
    void StageComplete ()
    {
        gameManagerAudioSource.PlayOneShot(stageCompleteMusic, 1.0f);
        currentStage++;
    }
}
