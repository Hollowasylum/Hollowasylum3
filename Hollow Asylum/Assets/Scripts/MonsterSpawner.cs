using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // The monster prefab to spawn
    public Transform leftSpawnPoint; // Left spawn point
    public Transform rightSpawnPoint; // Right spawn point

    // This function spawns a monster at a random location (left or right)
    public GameObject SpawnMonster()
    {
        // Randomly choose between left and right
        Transform chosenSpawnPoint = Random.value > 0.5f ? leftSpawnPoint : rightSpawnPoint;

        // Spawn the monster at the chosen position
        GameObject spawnedMonster = Instantiate(monsterPrefab, chosenSpawnPoint.position, Quaternion.identity);

        // Start the monster chasing Riley
        MonsterBehavior monsterBehavior = spawnedMonster.GetComponent<MonsterBehavior>();
        monsterBehavior.StartChasing();

        // Return the spawned monster so we can track it
        return spawnedMonster;
    }
}
