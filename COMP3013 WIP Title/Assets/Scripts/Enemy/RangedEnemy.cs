using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject projectile;
    public float spawnDistanceOffset;
    public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
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
        //Debug.Log("Chasing");
        agent.SetDestination(player.position);
        animator.SetTrigger("Walking"); // Set walking animation trigger
    }
    private void AttackPlayer()
    {
        //Debug.Log("Attacking");
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        animator.SetTrigger("Attacking");
    }

    public void SpawnProjectile()
    {
        if (!alreadyAttacked)
        {
            GameObject player = GameObject.FindWithTag("Player");

            Vector3 offset = transform.forward * spawnDistanceOffset;
            Vector3 spawnPosition = transform.position + offset;
            GameObject projectileInstance = Instantiate(projectile, spawnPosition, Quaternion.identity);
            Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            Destroy(projectileInstance, 2f);

            alreadyAttacked = true;
            animator.SetTrigger("Idle");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}