using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    // Assuming you have assigned Riley with a tag "Riley"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Riley"))
        {
            // Reset the game or implement your game over logic here
            Debug.Log("Monster touched Riley! Resetting the game...");
            // Insert your reset logic here, e.g. reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
