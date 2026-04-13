using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Animation mainCameraShake;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private GameManager gameManagerScript;
    public float jumpForce = 10;
    public float fastDownModifier;
    public bool fastDown_b;
    public bool isOnGround = true;


    public ParticleSystem deathParticle;
    public ParticleSystem runDirtParticle;
    public ParticleSystem crashDownParticle;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip fastDownSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set references to needed components
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCameraShake = GameObject.Find("Main Camera").GetComponent<Animation>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // check that game isn't over before handling further player input
        if (!gameManagerScript.gameOver)
        {
            // Player jump, animation, particles, jump sound
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                runDirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isOnGround)
            {
                playerRb.AddForce(Vector3.down * fastDownModifier, ForceMode.Impulse);

                fastDown_b = true;
            }
        }
    }

    // Any collision, collision object is passed to method
    private void OnCollisionEnter(Collision collision)
    {
        // When Player touches ground after jump
        if (collision.gameObject.CompareTag("Ground"))
        { 
            // prevents unexpected behavior if player hits obstacle before death
            if (!gameManagerScript.gameOver)
            {
                runDirtParticle.Play();
                isOnGround = true;
            }

            // handles fastdown effects if game isn't over
            if (fastDown_b && !gameManagerScript.gameOver)
            {
                crashDownParticle.Play();
                mainCameraShake.Play();
                playerAudio.PlayOneShot(fastDownSound, 1.0f);
                fastDown_b = false; // end of fast down effects
            }
            
        } 
        // When player touches obstacle
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            gameManagerScript.gameOver = true;  // tell game manager that game is over

            // handle player animation for death
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            runDirtParticle.Stop(); // stop running particle

            // Play death effects
            deathParticle.Play();
            playerAudio.PlayOneShot(deathSound, 1.0f);

            Debug.Log("Game Over");
        }
    }
}
