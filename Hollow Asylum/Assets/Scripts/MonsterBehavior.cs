using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;          // Riley's position (assign in Unity)
    public float moveSpeed = 3f;      // Monster's speed (slowed down)
    public AudioSource movementAudio; // Audio source for monster movement sound
    public float shakeDistance = 2f;   // Distance within which to shake the camera
    public float shakeDuration = 0.5f; // Duration of the camera shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private bool isChasing = false;   // Whether the monster is chasing Riley
    private CameraShake cameraShake;  // Reference to the CameraShake script

    void Start()
    {
        // Ensure the audio source is set to loop
        if (movementAudio != null)
        {
            movementAudio.loop = true; // Set the sound to loop
        }

        cameraShake = Camera.main.GetComponent<CameraShake>();
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

            // Check distance to the player and shake camera if close
            if (Vector2.Distance(transform.position, player.position) < shakeDistance)
            {
                if (cameraShake != null)
                {
                    cameraShake.ShakeCamera(shakeDuration, shakeMagnitude);
                }
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
