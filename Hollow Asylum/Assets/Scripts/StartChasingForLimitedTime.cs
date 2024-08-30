using UnityEngine;

public class MonsterTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Riley enters the trigger
        if (collision.CompareTag("Riley"))
        {
            Debug.Log("Riley entered the chase trigger!");

            // Find the monster in the scene with the "Monster" tag
            GameObject currentMonster = GameObject.FindWithTag("Monster");

            if (currentMonster != null)
            {
                // Get the MonsterBehavior component of the monster
                MonsterBehavior monsterBehavior = currentMonster.GetComponent<MonsterBehavior>();

                if (monsterBehavior != null)
                {
                    // Start the timed chase for 4.5 seconds
                    monsterBehavior.StartChasingForLimitedTime(4.5f);
                }
                else
                {
                    Debug.Log("MonsterBehavior component not found.");
                }
            }
            else
            {
                Debug.Log("Monster not found.");
            }
        }
    }
}
