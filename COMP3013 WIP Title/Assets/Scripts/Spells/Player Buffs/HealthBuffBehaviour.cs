using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealthBuffBehaviour : MonoBehaviour
{
    private GameObject player;
    private CharacterStats statsManager;
    public int maxHealthIncrease;
    public float buffDuration;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statsManager = player.GetComponent<CharacterStats>();
        StartCoroutine(TemporaryHealthIncrease(maxHealthIncrease, buffDuration));
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.E))
    //    {
          //StartCoroutine(TemporaryHealthIncrease(maxHealthIncrease, buffDuration)); //debug code
    //    }
    //}

    private IEnumerator TemporaryHealthIncrease(int healthIncrease, float buffDuration)
    {
        int originalMaxHealth = statsManager.maxHealth;
        Debug.Log("Original max health set. Value: " + originalMaxHealth);

        statsManager.maxHealth += healthIncrease;
        Debug.Log("New max health set. Value: " + statsManager.maxHealth);

        yield return new WaitForSeconds(buffDuration);

        Debug.Log("Time passed. Restoring max health.");

        statsManager.maxHealth = originalMaxHealth;

        //Destroy(gameObject);
    }
}