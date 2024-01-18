using UnityEngine;
using System.Collections.Generic;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    public List<GameObject> enemies = new List<GameObject>();
    private bool animationAlreadyPlayed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if all enemies are inactive or destroyed
        if (AreAllEnemiesDefeated() && !animationAlreadyPlayed)
        {
            // Trigger the door animation when all enemies are defeated
            animator.SetTrigger("DoorOpen");
            animationAlreadyPlayed = true;
        }
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