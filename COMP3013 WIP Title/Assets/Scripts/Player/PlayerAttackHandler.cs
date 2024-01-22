using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int swordDamage = 30;
    public GameObject player = null;
    private bool damageDealtInAnimationLoop = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if(!damageDealtInAnimationLoop)
        {
            animator.SetTrigger("Attacking");

            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in enemiesHit)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    BattleManager.Instance.Attack(player, enemy.gameObject, AttackType.SWORD);
                    Debug.Log("Damage dealt");
                    //enemy.GetComponent<EnemyHandler>().TakeDamage(swordDamage);
                }
            }

            damageDealtInAnimationLoop = true;
        }
    }

    void ResetDamageDealtFlag()
    {
        damageDealtInAnimationLoop = false;
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
