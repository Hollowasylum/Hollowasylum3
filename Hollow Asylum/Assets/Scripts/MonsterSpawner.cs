using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Spawn points for each fall trigger
    public Transform leftSpawnPoint1;
    public Transform rightSpawnPoint1;
    public Transform leftSpawnPoint2;
    public Transform rightSpawnPoint2;
    public Transform leftSpawnPoint3;
    public Transform rightSpawnPoint3;
    public Transform leftSpawnPoint4; // Shared by FallTrigger4 and FallTrigger5
    public Transform rightSpawnPoint6; // Special spawn point for FallTrigger6

    private GameObject currentMonster;
    private int fallTriggerCount = 0; // Tracks how many fall triggers have been hit

    public GameObject monsterPrefab; // Assign the monster prefab in the Inspector

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
                // Randomly spawn left or right for FallTrigger2
                spawnPosition = (Random.Range(0, 2) == 0) ? leftSpawnPoint2.position : rightSpawnPoint2.position;
                break;
            case 3:
                // Randomly spawn left or right for FallTrigger3
                spawnPosition = (Random.Range(0, 2) == 0) ? leftSpawnPoint3.position : rightSpawnPoint3.position;
                break;
            case 4:
            case 5:
                // Always spawn left for both FallTrigger4 and FallTrigger5 using LeftSpawnPoint4
                spawnPosition = leftSpawnPoint4.position;
                break;
            case 6:
                // Always spawn right for FallTrigger6
                spawnPosition = rightSpawnPoint6.position;
                break;
            default:
                Debug.LogWarning("No more fall triggers left!");
                return;
        }

        // Instantiate the new monster at the chosen spawn position
        currentMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // Start chasing Riley for a limited time (e.g., 9 seconds)
        MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();
        if (monsterBehavior != null)
        {
            monsterBehavior.StartChasingForLimitedTime(9f);
        }
        else
        {
            Debug.LogError("MonsterBehavior component not found on the spawned monster.");
        }
    }
}
