using UnityEngine;

public class FallTriggerScript : MonoBehaviour
{
    private MonsterSpawner spawner;

    private void Start()
    {
        // Find the MonsterSpawner in the scene
        spawner = FindObjectOfType<MonsterSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Riley"))
        {
            // Call the SpawnMonster method when Riley hits the fall trigger
            if (spawner != null)
            {
                spawner.SpawnMonster();
            }
            else
            {
                Debug.LogError("MonsterSpawner not found.");
            }
        }
    }
}
