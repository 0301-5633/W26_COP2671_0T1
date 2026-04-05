using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab; // is set inside of GameManager based on current stage
    public int maxSpawnHeight = 11;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawnPosCity = new Vector3(25, 0.6f, -0.6f);
    private Vector3 spawnPosTown = new Vector3(25, 0, 0);
    private int obstacleCount;
    private GameManager gameManagerScript;
    private StateManager stateManager;


    //=========================
    // Functions
    //=========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnObstacle());
    }

    // Update is called once per frame
    void Update()
    {
        // May Need to account for obstacles that don't fall of into delete zone
        //obstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
        //if (obstacleCount == 0)
        //{ 
        //}
    }

    IEnumerator SpawnObstacle()
    {
        //TODO: Will need to account for Pause
        while (!gameManagerScript.gameOver && !stateManager.pause)
        {
            yield return new WaitForSeconds(RandomSpawnDelay((float) stateManager.currentStage));
            if (gameManagerScript.gameOver) { break; } // Just incase death happens after the delay starts before spawning
            switch (stateManager.currentStage)
            {
                case StateManager.Stage.City:
                    Instantiate(obstaclePrefab, spawnPosCity, obstaclePrefab.transform.rotation);
                    break;
                case StateManager.Stage.Town:
                    Instantiate(obstaclePrefab, spawnPosTown, obstaclePrefab.transform.rotation);
                    break;
                case StateManager.Stage.Nature:
                    Instantiate(obstaclePrefab, RandomSpawnNature(), obstaclePrefab.transform.rotation);
                    break;
            }
        }
    }

    float RandomSpawnDelay(float stageModifier)
    {
        float spawnDelay = Random.Range(0.9f, 2);

        return spawnDelay;
    }
    Vector3 RandomSpawnNature()
    {
        int randomHeight = Random.Range(0, maxSpawnHeight);

        return new Vector3(25, randomHeight, 0);

    }
}
