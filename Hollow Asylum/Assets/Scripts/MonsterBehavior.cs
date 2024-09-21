using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;          // Riley's position (assign in Unity)
    public float moveSpeed = 3f;      // Monster's speed (slowed down)
    public AudioSource movementAudio; // Audio source for monster movement sound

    private bool isChasing = false;   // Whether the monster is chasing Riley

    void Start()
    {
        // Ensure the audio source is set to loop
        if (movementAudio != null)
        {
            movementAudio.loop = true; // Set the sound to loop
        }
    }

    void Update()
    {
        if (isChasing)
        {
            // Move towards Riley's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Play the movement sound if it's not already playing
            if (movementAudio != null && !movementAudio.isPlaying)
            {
                movementAudio.Play();
            }
        }
        else
        {
            // Stop the sound when the monster is not chasing
            if (movementAudio != null && movementAudio.isPlaying)
            {
                movementAudio.Stop();
            }
        }
    }

    public void StartChasing()
    {
        isChasing = true;

        // Start playing the movement sound if not already playing
        if (movementAudio != null && !movementAudio.isPlaying)
        {
            movementAudio.Play();
        }
    }

    public void StopChasing()
    {
        isChasing = false;

        // Stop playing the movement sound
        if (movementAudio != null && movementAudio.isPlaying)
        {
            movementAudio.Stop();
        }
    }

    // Start chasing for a limited time
    public void StartChasingForLimitedTime(float chaseDuration)
    {
        StartChasing(); // Start chasing Riley
        Invoke(nameof(StopChasing), chaseDuration); // Stop chasing after a delay
    }
}
