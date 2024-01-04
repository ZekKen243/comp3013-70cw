
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    FloatingHealthBar healthBar;
    private CardEnemyDrop cardDrop = null;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        cardDrop = GetComponent<CardEnemyDrop>();
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
        if(cardDrop)
        {
            cardDrop.DropCard();
        }
    }
}
