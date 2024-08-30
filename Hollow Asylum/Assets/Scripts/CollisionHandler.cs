using UnityEngine;
using UnityEngine.SceneManagement; // To reload the scene

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the monster collided with is Riley (the Player)
        if (collision.collider.CompareTag("Riley"))
        {
            // Reset the scene (reload the current scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
