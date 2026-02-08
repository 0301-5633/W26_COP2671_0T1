using UnityEngine;

public class PlayerController : MonoBehaviour
{
// First create the var in the class
private Animator playerAnim;
private Rigidbody playerRb;
// Don't forget that with particles you still have to add them to the Player object in the Unity Editor
public ParticleSystem explosionParticle;
public ParticleSystem dirtParticle;
public AudioSource playerAudio;
public AudioClip jumpSound;
public AudioClip youDead;
public AudioClip crashSound;
public float jumpForce = 10;
public float gravityModifier;
public bool isOnGround = true;
public bool gameOver;
public double scoreCounter;


        void Start()
    {
       playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // Then use the GetComponent method to get the component you're trying to access
        playerAnim = GetComponent<Animator>();
        // DO NOT FORGET TO GET COMPONENT FROM ANY COMPONENT YOU WANT TO ACCESS !!! 
        playerAudio = GetComponent<AudioSource>();
        InvokeRepeating("scoreCounterX", 0, 1);
    }
    public void scoreCounterX()
    {
        if (gameOver != true)
        {
            scoreCounter += 1000d;
            Debug.Log("Score: " + scoreCounter);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver != true) // or !gameOver
        {
            // Add force to the playerRb jump , ForceMode is the method of the force
            // which has 4 options: Force, Acceleration, Impulse, VelocityChange
            // Impulse is the best for jump because it gives a instant force
            playerRb.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            // the PlayOneShot method is used to play a sound once and not loop it , the 1.0f is the volume 
            playerAudio.PlayOneShot(jumpSound, 5.0f);
        
        }

        
       // transform.Translate(Vector3.forward * Time.deltaTime * 8);
    }
    private void OnCollisionEnter(Collision collision)
    {
        {

            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                dirtParticle.Play();
                
            } else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("rocket"))
            {
                Debug.Log("Game Over!");
                gameOver = true;
                // Sets Death_b to true 
                playerAnim.SetBool("Death_b", true);
                // Notice how the Set is set to the variable type . Also, don't forget to put the name in quotes
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                dirtParticle.Stop(); 
                playerAudio.PlayOneShot(crashSound, 5.0f);
                playerAudio.PlayOneShot(youDead, 1.0f);

            }
        }
    }
}   
