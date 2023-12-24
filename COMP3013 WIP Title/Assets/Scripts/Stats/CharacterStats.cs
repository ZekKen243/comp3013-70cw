using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat attack;
    public Stat defence;
    public float movementSpeed;
    public GameObject deathUI;
    public Slider healthBarUI;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarUI.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= defence.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        deathUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log(transform.name + "died");
    }
}
