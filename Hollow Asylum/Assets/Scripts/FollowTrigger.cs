using UnityEngine;

public class FollowTrigger : MonoBehaviour
{
    public MonsterAI monsterAI; // Reference to the MonsterAI script

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Riley") || other.CompareTag("Daniel"))
        {
            monsterAI.StartFollowing();
        }
    }
}
