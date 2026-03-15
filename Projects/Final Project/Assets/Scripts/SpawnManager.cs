using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab; // is set inside of GameManager based on current stage
    public int maxSpawnHeight = 11;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawnPosCity = new Vector3(25, 0.6f, -0.6f);
    private Vector3 spawnPosTown = new Vector3(25, 0, 0);
    
    private float startDelay = 2;
    private float repeateRate = 2;
    private int obstacleCount;
    private PlayerController playerControllerScript;
    private GameManager gameManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TODO: Will need to change to coroutine so that method can be called with parameters ie current stage
        InvokeRepeating("SpawnObstacle", startDelay, repeateRate);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    void SpawnObstacle()
    {
        //TODO: Will need to account for Pause
        if (playerControllerScript.gameOver == false)
        {
            switch (gameManager.currentStage)
            {
                case GameManager.Stage.City:
                    Instantiate(obstaclePrefab, spawnPosCity, obstaclePrefab.transform.rotation);
                    break;
                case GameManager.Stage.Town:
                    Instantiate(obstaclePrefab, spawnPosTown, obstaclePrefab.transform.rotation);
                    break;
                case GameManager.Stage.Nature:
                    Instantiate(obstaclePrefab, RandomSpawnNature(), obstaclePrefab.transform.rotation);
                    break;
            }
        }
    }

    Vector3 RandomSpawnNature()
    {
        int randomHeight = Random.Range(0, maxSpawnHeight);

        return new Vector3(25, randomHeight, 0);

    }
}
