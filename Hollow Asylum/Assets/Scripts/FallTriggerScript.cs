using UnityEngine;

public class FallTriggerScript : MonoBehaviour
{
    public MonsterSpawner monsterSpawner; // Reference to the MonsterSpawner script
    private GameObject currentMonster; // The currently spawned monster

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Riley enters the trigger
        if (collision.CompareTag("Riley"))
        {
            if (currentMonster != null)
            {
                // Stop the current monster from chasing
                MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();
                monsterBehavior.StopChasing();  // Stop the monster's movement
            }

            // Spawn a new monster and keep track of it
            currentMonster = monsterSpawner.SpawnMonster();
        }
    }
}
