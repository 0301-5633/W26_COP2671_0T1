using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObstacleVariantMovements : MonoBehaviour
{
    private Rigidbody targetRb;
    private float leftBound = -15;
    private float minSpeed = 40;
    private float maxSpeed = 50;
    private float maxTorque = 15;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(Vector3.left * maxSpeed, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.left * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(0, maxTorque);
    }
}
