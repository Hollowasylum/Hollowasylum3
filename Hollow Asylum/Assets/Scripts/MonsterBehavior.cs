using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player; // Riley's position (assign in Unity)
    public float moveSpeed = 5f; // How fast the monster moves

    private bool isChasing = false; // Controls whether the monster is chasing

    void Update()
    {
        if (isChasing)
        {
            // Move towards Riley's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Call this method to start chasing Riley for a limited time
    public void StartChasingForLimitedTime(float chaseDuration)
    {
        StartCoroutine(ChaseForTime(chaseDuration)); // Start the chase timer
    }

    // Coroutine to handle the timed chase
    IEnumerator ChaseForTime(float chaseDuration)
    {
        isChasing = true; // Start chasing
        yield return new WaitForSeconds(chaseDuration); // Wait for the specified time (e.g., 4.5 seconds)
        isChasing = false; // Stop chasing after the time is up
        Debug.Log("Chase stopped after " + chaseDuration + " seconds.");
    }
}
