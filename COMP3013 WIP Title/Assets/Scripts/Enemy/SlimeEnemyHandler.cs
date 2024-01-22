using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemyHandler : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Attacking }

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange; 
    public float chaseRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public int damage;
    public float attackCooldown = 2f;
    private bool canAttack = true;
    private EnemyState currentState = EnemyState.Patrolling;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            Chasing();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
                Debug.Log("Walkpoint set");
            }
            walkPointSet = true;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
       
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        // Inflict damage to the player
        player.GetComponent<GameEntity>().TakeDamage(damage);

        // Start the cooldown period
        canAttack = false;
        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}