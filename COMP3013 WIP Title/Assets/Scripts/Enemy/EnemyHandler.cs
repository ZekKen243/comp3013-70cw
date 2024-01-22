
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    FloatingHealthBar healthBar;
    private CardEnemyDrop cardDrop = null;
    private GameEntity gameEntity = null;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        cardDrop = GetComponent<CardEnemyDrop>();
        gameEntity = GetComponent<GameEntity>();
    }

    void OnEnable() 
    {
        gameEntity.OnTakeDamage += OnTakeDamage;
        gameEntity.OnDie += OnDie;
    }

    void OnDisable() 
    {
        gameEntity.OnTakeDamage -= OnTakeDamage;
        gameEntity.OnDie -= OnDie;
    }

    void OnTakeDamage(int damage, int currentHealth, int maxHealth)
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    void OnDie()
    {
        Die();
    }

    // Old code
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        gameObject.SetActive(false);
        if(cardDrop)
        {
            cardDrop.DropCard();
        }
    }
}
