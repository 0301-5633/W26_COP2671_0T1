using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public bool SENABLE = true;

    public GameObject obstaclePrefab; 
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

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnObstacle()
    {
        
        //TODO: Will need to account for Pause
        while (!gameManagerScript.gameOver && !stateManager.pause && SENABLE)
        {
            
            yield return new WaitForSeconds(RandomSpawnDelay((float) stateManager.currentStage));
            
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
