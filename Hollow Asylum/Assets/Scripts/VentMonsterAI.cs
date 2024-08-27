using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform riley;          // Riley's Transform
    public float followSpeed = 2f;   // Speed at which the monster follows Riley
    private Rigidbody2D rb2D;        // Rigidbody2D component for physics
    private bool shouldFollow = false; // Flag to control when the monster should follow Riley

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Initialize Rigidbody2D
        rb2D.gravityScale = 1; // Ensure gravity is enabled
    }

    void Update()
    {
        // Keep following Riley if the flag is set
        if (shouldFollow)
        {
            FollowRiley();
        }
    }

    void FollowRiley()
    {
        // Calculate the direction towards Riley
        Vector2 direction = (riley.position - transform.position).normalized;

        // Move horizontally towards Riley
        rb2D.velocity = new Vector2(direction.x * followSpeed, rb2D.velocity.y);

        // Ensure the monster falls naturally with gravity
        // No need to manually handle vertical movement; let gravity do its job
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the monster touches Riley
        if (collision.CompareTag("Riley"))
        {
            // Restart the scene or implement your game over logic
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartFollowing()
    {
        // Set the flag to start the following behavior
        shouldFollow = true;
    }
}
