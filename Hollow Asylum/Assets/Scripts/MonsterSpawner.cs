using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Assign the monster prefab in the Inspector
    public Transform[] spawnPoints;  // Array of spawn points
    private GameObject currentMonster;

    public void SpawnMonster()
    {
        // Safety check to prevent IndexOutOfRangeException
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the MonsterSpawner.");
            return; // Exit the method if no spawn points are available
        }

        // Spawn the monster at a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector2 spawnPosition = spawnPoints[randomIndex].position;

        // Destroy the current monster if one exists (optional)
        if (currentMonster != null)
        {
            Destroy(currentMonster);
        }

        // Instantiate the new monster
        currentMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // Get the MonsterBehavior component of the newly spawned monster
        MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();

        if (monsterBehavior != null)
        {
            // Start the timed chase for 4.5 seconds
            monsterBehavior.StartChasingForLimitedTime(6f);
        }
        else
        {
            Debug.LogError("MonsterBehavior component not found on the spawned monster.");
        }
    }
}
