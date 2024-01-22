using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public int damage;
    public Transform attackPoint;
    public float attackRangeSword = 0.5f;
    public LayerMask playerLayer;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public Animator animator; 
    private bool damageDealtInAnimationLoop;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            walkPointSet = true;
            animator.SetTrigger("Walking"); // Set walking animation trigger
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetTrigger("Idle"); // Set idle animation trigger when reaching the walk point
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetTrigger("Walking"); // Set walking animation trigger
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        // Calculate the direction to the player without affecting the vertical rotation
        Vector3 playerDirection = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;

        // Rotate the enemy's body to face the player
        transform.rotation = Quaternion.LookRotation(playerDirection);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attacking"); // Set attacking animation trigger
            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
            Debug.Log("Reset coroutine enabled");
        }
    }

    public void Attack()
    {
        Collider[] playerHit = Physics.OverlapSphere(attackPoint.position, attackRangeSword, playerLayer);

        foreach (Collider player in playerHit)
        {
            if (player.CompareTag("Player") && !damageDealtInAnimationLoop)
            {
               // player.GetComponent<GameEntity>().TakeDamage(damage);
                BattleManager.Instance.Attack(gameObject, GameObject.Find("Player"), AttackType.SWORD);

                damageDealtInAnimationLoop = true;
                Debug.Log("Player attacked!");
                animator.SetTrigger("Idle");
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("Walkpoint set");
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
        Debug.Log("alreadyAttacked has been reset");
    }

    // Animation Event
    public void DamageFlagReset()
    {
        damageDealtInAnimationLoop = false;
        Debug.Log("Damage flag has been reset");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}