using UnityEngine;

public class MonsterStopTriggerScript : MonoBehaviour
{
    private GameObject currentMonster; // Reference to the current monster in the scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Riley enters the trigger
        if (collision.CompareTag("Riley"))
        {
            // Find the monster in the scene (if not already assigned)
            currentMonster = GameObject.FindWithTag("Monster"); // Ensure the monster has the "Monster" tag

            if (currentMonster != null)
            {
                // Stop the monster from chasing
                MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();
                monsterBehavior.StopChasing();  // Stop the monster's movement
            }
        }
    }
}
