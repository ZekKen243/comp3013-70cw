using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    FloatingHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        gameObject.SetActive(false);
    }
}
