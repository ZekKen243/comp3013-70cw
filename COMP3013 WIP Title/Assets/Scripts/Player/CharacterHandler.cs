
using UnityEngine;
using UnityEngine.UI;

public class CharacterHandler : MonoBehaviour
{
    public GameObject deathUI;
    public Slider healthBarUI;

    private GameEntity gameEntity = null;

    void Awake()
    {
        gameEntity = GetComponent<GameEntity>();
        InitListeners();
    }

    void InitListeners()
    {
        if(gameEntity == null)
        {
            Debug.LogError("Failed to init listeners, no GameEntity script found.");
            return;
        }

        gameEntity.OnDie += OnDie;
        gameEntity.OnTakeDamage += OnTakeDamage;
    }

    void OnDie()
    {
        if(deathUI == null)
        {
            Debug.LogError("Failed to show death UI, null reference.");
            return;
        }

        deathUI.SetActive(true);
    }

    void OnTakeDamage(int damage, int currentHealth, int maxHealth)
    {
        if(healthBarUI == null)
        {
            Debug.LogError("Failed to update player health bar, null reference.");
            return;
        }

        Debug.LogFormat("AAAAAAAAAAAAAAAAAAAAAAAAAAAA {0}",  (float) currentHealth / (float) maxHealth);
        healthBarUI.value = (float) currentHealth / (float) maxHealth;
    }
}
