using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 3f;     // Normal movement speed
    public float sprintSpeed = 5f;     // Speed when Shift is pressed
    public float moveSpeed;            // Current movement speed
    public float smoothTime = 0.1f;    // Smooth time for velocity change

    private Rigidbody2D rb;            // Rigidbody2D component for movement
    private Vector2 velocity = Vector2.zero; // For smooth movement
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Check if Shift is pressed to set the movement speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        // Check if the player is grounded
        isGrounded = CheckIfGrounded();
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

    bool CheckIfGrounded()
    {
        // Perform a small raycast to determine if the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
