using UnityEngine;

public class camScript : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public AudioSource musicX;
    public AudioClip deathSound;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        musicX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            musicX.Stop();
           musicX.PlayOneShot(deathSound, 1.0f);
        }
    }
}
