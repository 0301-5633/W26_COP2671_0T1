using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawnPos4rocket = new Vector3(27, 5 , 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private float startDelayR = 3;
    private float repeatRateR = 4;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnRocket", startDelayR, repeatRateR);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false) 
        {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
void SpawnRocket()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(rocketPrefab, spawnPos4rocket, rocketPrefab.transform.rotation);
        }
    }
}

