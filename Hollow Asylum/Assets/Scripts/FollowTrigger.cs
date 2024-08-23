using UnityEngine;
using Cinemachine;
using System.Collections;

public class FollowTrigger : MonoBehaviour
{
    public MonsterAI monsterAI;              // Reference to the MonsterAI script
    public CinemachineVirtualCamera rileyCamera;  // Reference to the Cinemachine camera for Riley
    public CinemachineVirtualCamera monsterCamera; // Reference to the Cinemachine camera for Monster

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Riley") || other.CompareTag("Daniel"))
        {
            // Start the camera switch sequence
            StartCoroutine(SwitchCameraToMonster());
        }
    }

    private IEnumerator SwitchCameraToMonster()
    {
        // Switch to Monster camera
        monsterCamera.Priority = 10;
        rileyCamera.Priority = 0;

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Switch back to Riley camera
        monsterCamera.Priority = 0;
        rileyCamera.Priority = 10;

        // Start the monster following after camera switch
        monsterAI.StartFollowing();
    }
}
