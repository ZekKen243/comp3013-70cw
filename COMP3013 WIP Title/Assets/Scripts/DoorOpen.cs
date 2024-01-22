using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    private bool animationAlreadyPlayed = false;
    public GameObject victoryUI;

    private void Start()
    {
    }

    void Update()
    {
        // Check if all enemies are inactive or destroyed
        if (AreAllEnemiesDefeated() && !animationAlreadyPlayed)
        {
            Debug.Log("Trigger enter detected");
            victoryUI.SetActive(true);
            Debug.Log("Should be loading scene");

        }

        bool AreAllEnemiesDefeated()
        {
            // Check each enemy in the list
            foreach (GameObject enemy in enemies)
            {
                // If any enemy is active, return false
                if (enemy.activeSelf)
                {
                    return false;
                }
            }

            // If no active enemies are found, return true
            return true;
        }
    }
}