using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyProjectileBehaviour : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

    }
}
