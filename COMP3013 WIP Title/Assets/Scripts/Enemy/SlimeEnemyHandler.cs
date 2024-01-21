using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemyHandler : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing, Attacking }

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;  // Add this line for ground detection
    public float patrolRange;
    public float chaseRange;
    public float attackRange;
    public int damage;
    public float attackCooldown = 2f;
    private bool canAttack = true;
    private Vector3 originalPosition;
    private Vector3 patrolDestination;
    private EnemyState currentState = EnemyState.Patrolling;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        SetRandomPatrolDestination();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrolling();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.Attacking:
                AttackPlayer();
                break;
        }
    }

    private void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomPatrolDestination();
        }

        // Check if the player is within the chase range
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = EnemyState.Chasing;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);

        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            currentState = EnemyState.Attacking;
        }

        // Check if the player is outside the chase range
        if (Vector3.Distance(transform.position, player.position) > chaseRange)
        {
            currentState = EnemyState.Patrolling;
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        // Inflict damage to the player
        player.GetComponent<GameEntity>().TakeDamage(damage);

        // Start the cooldown period
        canAttack = false;
        Invoke(nameof(ResetAttackCooldown), attackCooldown);

        // Transition back to chasing after attacking
        currentState = EnemyState.Chasing;
    }

    private void SetRandomPatrolDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += originalPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, whatIsGround))  // Use whatIsGround for NavMesh.SamplePosition
        {
            patrolDestination = hit.position;
            agent.SetDestination(patrolDestination);
        }
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(patrolDestination, 0.2f);
    }
}