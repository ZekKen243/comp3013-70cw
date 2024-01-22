using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyProjectileBehaviour : MonoBehaviour
{
    public GameObject attacker;
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BattleManager.Instance.Attack(attacker, other.gameObject, AttackType.MAGIC);
            //other.GetComponent<GameEntity>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

    }
}
