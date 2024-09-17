using UnityEngine;
using UnityEngine.SceneManagement; // To reload the scene
using System.Collections; // For using coroutines

public class CollisionHandler : MonoBehaviour
{
    public AudioSource jumpscareAudio; // Assign this in the Inspector (jumpscare sound)
    public AudioSource chaseMusic;     // Assign this in the Inspector (chase music)
    public float delayBeforeReset = 3.4f; // Time to wait before resetting (3.4 seconds)
    private bool hasCollided = false;  // To prevent multiple triggers

    // References to movement scripts
    public PlayerMovement playerMovement; // Assign this in the Inspector
    public MonsterBehavior monsterBehavior; // Assign this in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the monster collided with is Riley (the Player)
        if (collision.collider.CompareTag("Riley") && !hasCollided)
        {
            hasCollided = true; // Prevent multiple triggers before reset
            Debug.Log("Collision detected with Riley.");

            // Stop the chase music if it's playing
            if (chaseMusic != null && chaseMusic.isPlaying)
            {
                Debug.Log("Stopping chase music.");
                chaseMusic.Stop();
            }
            else
            {
                Debug.Log("Chase music is not playing.");
            }

            // Play the jumpscare sound
            if (jumpscareAudio != null)
            {
                Debug.Log("Playing jumpscare sound.");
                jumpscareAudio.Play();
            }
            else
            {
                Debug.LogError("Jumpscare audio source is not assigned.");
            }

            // Stop the player and monster movement
            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Disable player movement script
                Debug.Log("Player movement stopped.");
            }
            else
            {
                Debug.LogError("PlayerMovement script is not assigned.");
            }

            if (monsterBehavior != null)
            {
                monsterBehavior.StopChasing(); // Stop the monster chasing behavior
                Debug.Log("Monster chasing stopped.");
            }
            else
            {
                Debug.LogError("MonsterBehavior script is not assigned.");
            }

            // Start the coroutine to wait for the reset
            StartCoroutine(ResetAfterDelay());
        }
        else
        {
            if (!collision.collider.CompareTag("Riley"))
            {
                Debug.Log("Collision detected with something else: " + collision.collider.name);
            }
            if (hasCollided)
            {
                Debug.Log("Already collided, ignoring further collisions.");
            }
        }
    }

    // Coroutine to handle the delay before resetting the scene
    IEnumerator ResetAfterDelay()
    {
        Debug.Log("Starting reset countdown of " + delayBeforeReset + " seconds.");
        // Wait for the duration of the jumpscare sound or a fixed delay
        yield return new WaitForSeconds(delayBeforeReset);

        Debug.Log("Resetting the scene now.");
        // Reset the scene after the delay
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // After the scene reloads, allow future collisions again
        hasCollided = false;
        Debug.Log("Scene reset complete, ready for new collisions.");
    }
}
