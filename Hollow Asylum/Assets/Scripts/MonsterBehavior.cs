using UnityEngine;

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

    public void StartChasing()
    {
        isChasing = true; // Start chasing Riley
    }

    public void StopChasing()
    {
        isChasing = false; // Stop chasing Riley
        // Optionally, disable the monster here
        this.gameObject.SetActive(false); // Hide the monster if needed after stopping
    }
}
