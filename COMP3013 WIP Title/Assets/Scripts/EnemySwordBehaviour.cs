using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordBehaviour : MonoBehaviour
{
    public bool damageDealtInAnimationLoop;
    public int damage;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public void Attack()
    {
        if (!damageDealtInAnimationLoop)
        {
            animator.SetTrigger("Attack");

            Collider[] playerHit = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider player in playerHit)
            {
                if (player.CompareTag("Player"))
                {
                    player.GetComponent<CharacterStats>().TakeDamage(damage);
                }


                damageDealtInAnimationLoop = true;
            }
        }
    }

    void ResetDamageDealtFlag()
    {
        damageDealtInAnimationLoop = false;
    }

    void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

