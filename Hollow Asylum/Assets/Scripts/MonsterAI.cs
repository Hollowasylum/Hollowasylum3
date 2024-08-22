using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public Transform riley;      // Reference to Riley's Transform
    public Transform daniel;     // Reference to Daniel's Transform
    public float followSpeed = 2f; // Speed at which the monster follows

    private bool shouldFollow = false;

    void Update()
    {
        if (shouldFollow)
        {
            Transform target = GetClosestCharacter();
            FollowTarget(target);
        }
    }

    Transform GetClosestCharacter()
    {
        float distanceToRiley = Vector2.Distance(transform.position, riley.position);
        float distanceToDaniel = Vector2.Distance(transform.position, daniel.position);

        return distanceToRiley < distanceToDaniel ? riley : daniel;
    }

    void FollowTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Riley"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void StartFollowing()
    {
        shouldFollow = true;
    }
}
