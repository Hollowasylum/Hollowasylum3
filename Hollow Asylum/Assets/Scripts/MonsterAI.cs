using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public Transform riley;      // Reference to Riley's Transform
    public float followSpeed = 2f; // Speed at which the monster follows

    private bool shouldFollow = false; // Determines if the monster should follow

    void Update()
    {
        if (shouldFollow)
        {
            // Always follow Riley
            FollowRiley();
        }
    }

    void FollowRiley()
    {
        // Move the monster toward Riley's position
        transform.position = Vector2.MoveTowards(transform.position, riley.position, followSpeed * Time.deltaTime);
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
