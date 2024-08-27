using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public Transform riley;      // Reference to Riley's Transform
    public float followSpeed = 2f; // Speed at which the monster follows
    public LayerMask obstacleMask; // LayerMask for walls/obstacles

    private bool shouldFollow = false; // Determines if the monster should follow

    void Update()
    {
        if (shouldFollow)
        {
            FollowRiley();
        }
    }

    void FollowRiley()
    {
        // Calculate direction towards Riley
        Vector2 direction = (riley.position - transform.position).normalized;

        // Check if there's an obstacle (wall) in the way
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, obstacleMask);

        // Only move if there is no wall directly in front
        if (!hit.collider)
        {
            transform.position = Vector2.MoveTowards(transform.position, riley.position, followSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Reset the game if the monster touches Riley
        if (collision.CompareTag("Riley"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void StartFollowing()
    {
        // Start the monster's movement towards Riley
        shouldFollow = true;
    }
}