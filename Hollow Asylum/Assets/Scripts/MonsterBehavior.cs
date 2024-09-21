using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;          // Riley's position (assign in Unity)
    public float moveSpeed = 3f;      // Monster's speed (slowed down)
    public AudioSource movementAudio; // Audio source for monster movement sound
    public float soundPlayInterval = 0.3f; // Interval for playing the sound when moving

    private bool isChasing = false;   // Whether the monster is chasing Riley
    private float nextSoundTime = 0f; // Time when the next sound should play

    void Update()
    {
        if (isChasing)
        {
            // Move towards Riley's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Play the movement sound at intervals while the monster is moving
            if (Time.time >= nextSoundTime)
            {
                PlayMovementSound();
                nextSoundTime = Time.time + soundPlayInterval; // Set next sound time
            }
        }
    }

    // Play the movement sound
    void PlayMovementSound()
    {
        if (movementAudio != null && !movementAudio.isPlaying)
        {
            movementAudio.Play();
        }
    }

    public void StartChasing()
    {
        isChasing = true;
        nextSoundTime = Time.time; // Reset sound timer when chasing starts
    }

    public void StopChasing()
    {
        isChasing = false;
        if (movementAudio != null && movementAudio.isPlaying)
        {
            movementAudio.Stop(); // Stop the sound when the monster stops moving
        }
    }

    // Start chasing for a limited time
    public void StartChasingForLimitedTime(float chaseDuration)
    {
        StartChasing(); // Start chasing Riley
        Invoke(nameof(StopChasing), chaseDuration); // Stop chasing after a delay
    }
}
