using UnityEngine;

public class FallTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Riley"))
        {
            // Find the MonsterSpawner script
            MonsterSpawner spawner = FindObjectOfType<MonsterSpawner>();

            if (spawner != null)
            {
                // Spawn the monster
                spawner.SpawnMonster();
            }
            else
            {
                Debug.Log("MonsterSpawner not found.");
            }
        }
    }
}
