using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public Transform riley;  // Reference to Riley's transform
    public float followDistance = 1f; // The minimum distance to maintain from Riley
    private Vector3 lastRileyPosition; // To track Riley's last position
    private Vector3 rileyVelocity; // To track Riley's velocity

    void Start()
    {
        // Initialize Riley's last position
        if (riley != null)
        {
            lastRileyPosition = riley.position;
        }
    }

    void Update()
    {
        // Ensure Riley exists before proceeding
        if (riley != null)
        {
            // Calculate the distance between Daniel and Riley
            float distanceToRiley = Vector2.Distance(transform.position, riley.position);

            // Calculate Riley's velocity (speed and direction)
            rileyVelocity = (riley.position - lastRileyPosition) / Time.deltaTime;

            // Check if Daniel is above or below Riley due to a fall
            if (riley.position.y != lastRileyPosition.y)
            {
                // If Riley moves vertically, allow Daniel to follow immediately
                transform.position = Vector2.MoveTowards(transform.position, riley.position, rileyVelocity.magnitude * Time.deltaTime);
            }
            else if (distanceToRiley > followDistance)
            {
                // Follow Riley normally when the distance is greater than the follow distance
                transform.position = Vector2.MoveTowards(transform.position, riley.position, rileyVelocity.magnitude * Time.deltaTime);
            }

            // Update Riley's last known position
            lastRileyPosition = riley.position;
        }
    }
}
