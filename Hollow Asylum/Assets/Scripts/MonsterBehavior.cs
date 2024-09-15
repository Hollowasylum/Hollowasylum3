using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player; // Riley's position (assign in Unity)
    public float moveSpeed = 5f; // Monster's speed
    private bool isChasing = false;

    public AudioSource jumpscareAudio; // Assign in Inspector
    public float resetDelay = 1f; // Delay before resetting the scene

    private bool hasTriggeredJumpscare = false;

    void Update()
    {
        if (isChasing && !hasTriggeredJumpscare)
        {
            // Move towards Riley's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredJumpscare)
        {
            hasTriggeredJumpscare = true; // Prevent multiple triggers

            if (jumpscareAudio != null)
            {
                jumpscareAudio.Play();
                // Disable player movement temporarily
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }

                // Wait until the audio finishes playing before resetting
                Invoke(nameof(RestartGame), jumpscareAudio.clip.length + resetDelay);
            }
            else
            {
                Debug.LogError("Jumpscare AudioSource not assigned!");
                RestartGame(); // Fallback to reset if no audio found
            }
        }
    }

    public void StartChasing()
    {
        isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
    }

    public void StartChasingForLimitedTime(float chaseDuration)
    {
        StartChasing(); // Start chasing Riley
        Invoke(nameof(StopChasing), chaseDuration); // Stop chasing after a delay
    }

    void RestartGame()
    {
        // Reset the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
