using UnityEngine;
using System.Collections;  // Add this line for IEnumerator

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 3f;        // Normal movement speed
    public float sprintSpeed = 5f;        // Speed when Shift is pressed
    public float moveSpeed;               // Current movement speed
    public float smoothTime = 0.1f;       // Smooth time for velocity change
    public float crawlSoundInterval = 1.7f;  // Time between crawl sounds (normal)
    public float sprintCrawlSoundInterval = 0.8f; // Time between crawl sounds (sprinting)

    private Rigidbody2D rb;               // Rigidbody2D component for movement
    private Vector2 velocity = Vector2.zero; // For smooth movement
    private bool isTouchingWall;          // To check if Riley is touching a wall

    public AudioSource crawlAudio;        // Crawl sound
    private bool isMoving = false;        // To check if player is moving
    private bool canPlayCrawlSound = true;// To control crawl sound playback

    private bool canSprint = false;       // Initially, Riley can't sprint

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        moveSpeed = normalSpeed;          // Set the initial movement speed to normal
    }

    void Update()
    {
        // Check if sprinting is allowed
        if (canSprint && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        // Check if the player is touching a wall
        isTouchingWall = CheckIfTouchingWall();

        // Check if Riley is moving
        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;

        // If moving and can play sound, start the coroutine
        if (isMoving && canPlayCrawlSound)
        {
            StartCoroutine(PlayCrawlSound());
        }
    }

    void FixedUpdate()
    {
        // Get input values for movement
        float moveX = Input.GetAxis("Horizontal");

        // Create a smooth velocity for horizontal movement
        Vector2 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Apply smooth movement to Rigidbody2D
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
    }

    // Method to detect if Riley is touching a wall
    bool CheckIfTouchingWall()
    {
        // Perform a small raycast to detect walls (either left or right)
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.2f, LayerMask.GetMask("Walls"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.2f, LayerMask.GetMask("Walls"));

        if (hitLeft.collider != null || hitRight.collider != null)
        {
            Debug.Log("Touching Wall: true");
            return true;
        }
        else
        {
            Debug.Log("Touching Wall: false");
            return false;
        }
    }

    // Coroutine to play the crawl sound based on movement type (normal or sprinting)
    IEnumerator PlayCrawlSound()
    {
        canPlayCrawlSound = false; // Prevent the sound from playing too often

        // Play the crawl sound
        if (crawlAudio != null)
        {
            crawlAudio.Play();
        }

        // Check if the player is sprinting, and adjust sound interval accordingly
        if (moveSpeed == sprintSpeed)
        {
            yield return new WaitForSeconds(sprintCrawlSoundInterval); // Sprinting sound interval
        }
        else
        {
            yield return new WaitForSeconds(crawlSoundInterval); // Normal walking sound interval
        }

        canPlayCrawlSound = true; // Allow the sound to play again
    }

    // Method to unlock sprinting
    public void UnlockSprinting()
    {
        canSprint = true;
    }
}
