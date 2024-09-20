using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player; // Riley's position (assign in Unity)
    public float moveSpeed = 5f; // Monster's speed
    private bool isChasing = false;

    public AudioSource movementAudio; // AudioSource for the monster's movement sound
    public float slowPitch = 0.7f; // Adjust pitch to slow down sound

    void Start()
    {
        // Set the pitch to slow down the movement sound
        if (movementAudio != null)
        {
            movementAudio.pitch = slowPitch;
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
            if (!movementAudio.isPlaying)
            {
                movementAudio.loop = true; // Ensure the sound loops
                movementAudio.Play();
            }
        }
        else
        {
            // Stop the movement sound when not chasing
            if (movementAudio.isPlaying)
            {
                movementAudio.Stop();
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

    // Start chasing for a limited time
    public void StartChasingForLimitedTime(float chaseDuration)
    {
        StartChasing(); // Start chasing Riley
        Invoke(nameof(StopChasing), chaseDuration); // Stop chasing after a delay
    }
}
