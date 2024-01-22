using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTwoToAreaThreeTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter detected");

        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Load the desired scene
            SceneManager.LoadScene("Boss Lair - Area Three");
            Debug.Log("Should be loading scene");
        }
    }
}
