using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Stage
    {
        City, Town, Nature
    }

    public List<Sprite> backgroundsList;
    public List<GameObject> obstaclesList;
    public Stage currentStage;
    private SpawnManager spawnManagerScript;
    private SpriteRenderer stageBackground;
    


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set variables to needed objects/components
        stageBackground = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        // Set current stage and set up stage assets
        //currentStage = Stage.City;
        StageSetup(currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: GameManager, Coroutine, Stage timer milestone
    // - Stage timer
    // - End of stage Logic (increment currentStage, call stage setup, start next stage)
    //

    // Called to set up obstacles and background at the beginning of each stage
    void StageSetup(Stage currStage)
    {
        stageBackground.sprite = backgroundsList[(int)currentStage];
        spawnManagerScript.obstaclePrefab = obstaclesList[(int)currentStage];
        
    }
}
