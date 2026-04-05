using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Animation mainCameraShake;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private GameManager gameManagerScript;
    public float gravityModifier;
    public float jumpForce = 10;
    public float fastDownModifier;
    public bool fastDown_b;
    public bool isOnGround = true;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem crashDownParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip fastDownSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCameraShake = GameObject.Find("Main Camera").GetComponent<Animation>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player jump, animation, particles, jump sound
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameManagerScript.gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isOnGround && !gameManagerScript.gameOver)
        {
            playerRb.AddForce(Vector3.down * fastDownModifier, ForceMode.Impulse);
            
            fastDown_b = true;
        }
    }

    // Any collision, collision object is passed to method
    private void OnCollisionEnter(Collision collision)
    {
        // When Player touches ground after jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
            // prevents particle play if player hits obstacle before death
            if (!gameManagerScript.gameOver)
            {
                dirtParticle.Play();
            }

            // handles fastdown effects
            if (fastDown_b)
            {
                crashDownParticle.Play();
                mainCameraShake.Play();
                playerAudio.PlayOneShot(fastDownSound, 1.0f);
                fastDown_b = false;
            }
            
        } 
        // When player touches obstacle
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            gameManagerScript.gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
