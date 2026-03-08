using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animation mainCameraShake;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public float gravityModifier;
    public float jumpForce = 10;
    public float fastDownModifier = 10;
    public bool fastDown_b;
    public bool isOnGround = true;
    public bool gameOver;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem crashDownParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip fastDownSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isOnGround && !gameOver)
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
            if (!gameOver)
            {
                dirtParticle.Play();
            }

            // handles fastdown effects
            if (fastDown_b)
            {
                crashDownParticle.Play();
                mainCameraShake.Play();
                fastDown_b = false;
            }
            
        } 
        // When player touches obstacle
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
