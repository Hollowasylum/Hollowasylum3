using System.Collections; // Required for IEnumerator and coroutines
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Singleton instance of MonsterSpawner
    public static MonsterSpawner Instance { get; private set; }

    // Spawn points for each fall trigger
    public Transform leftSpawnPoint1;
    public Transform rightSpawnPoint1;
    public Transform leftSpawnPoint3;
    public Transform rightSpawnPoint3;
    public Transform leftSpawnPoint4; // Used by FallTrigger5

    private GameObject currentMonster;
    private int fallTriggerCount = 0; // Tracks how many fall triggers have been hit

    public GameObject monsterPrefab; // Assign the monster prefab in the Inspector

    // Audio for chase music and background music
    public AudioSource chaseMusicSource;    // Assign in the Inspector
    public AudioSource backgroundMusicSource; // Assign in the Inspector

    private void Awake()
    {
        // Singleton pattern setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnMonster()
    {
        // Increment the fall trigger count
        fallTriggerCount++;

        // Destroy the current monster if one exists (optional)
        if (currentMonster != null)
        {
            Destroy(currentMonster);
        }

        // Determine spawn location based on which fall trigger we're on
        Vector2 spawnPosition = Vector2.zero;

        switch (fallTriggerCount)
        {
            case 1:
                // Randomly spawn left or right for FallTrigger1
                spawnPosition = (Random.Range(0, 2) == 0) ? leftSpawnPoint1.position : rightSpawnPoint1.position;
                break;
            case 2:
                // Randomly spawn left or right for FallTrigger3
                spawnPosition = (Random.Range(0, 2) == 0) ? leftSpawnPoint3.position : rightSpawnPoint3.position;
                break;
            case 3:
                // Always spawn left for FallTrigger5 using LeftSpawnPoint4
                spawnPosition = leftSpawnPoint4.position;
                break;
            default:
                Debug.LogWarning("No more fall triggers left!");
                return;
        }

        // Instantiate the new monster at the chosen spawn position
        currentMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // Start chasing Riley for a limited time (e.g., 7.5 seconds)
        MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();
        if (monsterBehavior != null)
        {
            monsterBehavior.StartChasingForLimitedTime(7.5f);
        }
        else
        {
            Debug.LogError("MonsterBehavior component not found on the spawned monster.");
        }

        // Stop the background music
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        // Play chase music
        if (chaseMusicSource != null && !chaseMusicSource.isPlaying)
        {
            chaseMusicSource.Play();
        }
    }
}
