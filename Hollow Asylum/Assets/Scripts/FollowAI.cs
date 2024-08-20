using UnityEngine;

public class AdvancedFollowAI : MonoBehaviour
{
    public Transform target;               // The target to follow (Riley)
    public float followSpeed = 3f;         // Speed when following the target
    public float sprintSpeed = 5f;         // Speed when sprinting
    public float smoothTime = 0.3f;        // Time to smooth the movement

    private Rigidbody2D rb;                // Rigidbody2D component for movement
    private PlayerMovement targetMovement; // Reference to Riley's PlayerMovement script
    private Vector2 velocity = Vector2.zero; // Used for smoothing the movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Get the Rigidbody2D component
        if (target != null)
        {
            targetMovement = target.GetComponent<PlayerMovement>(); // Get Riley's PlayerMovement script
        }
    }

    void FixedUpdate()
    {
        if (target != null && targetMovement != null)
        {
            // Calculate direction to the target
            Vector2 direction = (target.position - transform.position).normalized;

            // Get Riley's current speed (includes sprint speed)
            float targetSpeed = targetMovement.moveSpeed;

            // Calculate target velocity based on Riley's speed
            Vector2 targetVelocity = direction * targetSpeed;

            // Smoothly move towards the target
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        }
    }
}
