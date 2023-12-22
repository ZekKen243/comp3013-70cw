using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private EnemyHandler enemyHandler;
    Animator animator;
    public GameObject sword;

    public int phaseThreshold = 150; // health required for boss to transition into phase 2
    public float delayBeforePhase2 = 2f; // allows for phase transition animation, adjust as needed
    public float attackRange = 5f; // distance at which close-range attacks will trigger
    public float minCooldownTime = 3f;
    public float maxCooldownTime = 6f;

    private bool isPhase2 = false;
    private bool isInPhase2Transition = false;
    private float attackCooldown = 0f;

    private bool isGroundSlamAnimFinished;
    public GameObject groundSlamProjectilePrefab;
    public Transform groundSlamProjectileSpawnPoint;

    public GameObject rangedAttackProjectilePrefab;
    public Transform[] projectileSpawnPoints;

    public GameObject[] summoningEnemyPrefabs;
    public Transform[] summoningSpawnPoints;

    // Define attack methods for each phase
    private System.Action[] phase1CloseAttacks;
    private System.Action[] phase2CloseAttacks;

    void Start()
    {
        player = GameObject.Find("Player").transform; // Adjust accordingly
        agent = GetComponent<NavMeshAgent>();
        enemyHandler = GetComponent<EnemyHandler>();
        animator = GetComponent<Animator>();

        // initialize close-range attack methods for each phase
        phase1CloseAttacks = new System.Action[]
        {
            GroundSlamAttackStart,
            CloseRangeHorizontalSwordAttack
        };

        phase2CloseAttacks = new System.Action[]
        {
            CloseRangeSummonMinionsAttack,
            CloseRangeSwordSpinAttack
        };
    }

    void Update()
    {
        // Set destination to follow the player
        if (!isInPhase2Transition)
        {
            agent.SetDestination(player.position);
        }

        if (enemyHandler.currentHealth <= phaseThreshold)
        {
            if (!isPhase2)
            {
                StartCoroutine(Phase2Routine());
            }
            else
            {
                // in Phase 2, loop through attacks based on distance to player
                if (attackCooldown <= 0f)
                {
                    if (Vector3.Distance(transform.position, player.position) > attackRange)
                    {
                        // only one long-range attack in phase 2, so no need to randomly iterate through array
                        FarRangeProjectileBarrageAttack();
                    }
                    else
                    {
                        // randomly select a close-range attack in Phase 2
                        int randomIndex = Random.Range(0, phase2CloseAttacks.Length);
                        phase2CloseAttacks[randomIndex]?.Invoke();
                    }

                    // set the cooldown
                    attackCooldown = Random.Range(minCooldownTime, maxCooldownTime);
                }

                // update cooldown
                attackCooldown = Mathf.Max(0f, attackCooldown - Time.deltaTime);
            }
        }
        else
        {
            if (attackCooldown <= 0f)
            {
                if (Vector3.Distance(transform.position, player.position) > attackRange)
                {
                    // only one long-range attack in phase 2, so no need to randomly iterate through array
                    FarRangeMultipleProjectileAttack();
                }
                else
                {
                    int randomIndex = Random.Range(0, phase1CloseAttacks.Length);
                    phase1CloseAttacks[randomIndex]?.Invoke();
                }

                attackCooldown = Random.Range(minCooldownTime, maxCooldownTime);
            }

            attackCooldown = Mathf.Max(0f, attackCooldown - Time.deltaTime);
        }
    }

    IEnumerator Phase2Routine()
    {
        isInPhase2Transition = true;
        agent.SetDestination(transform.position); //freezes boss so that their phase change animation looks cleaner
        Debug.Log("Going into phase 2");
        yield return new WaitForSeconds(delayBeforePhase2);
        // trigger phase transition animation
        isPhase2 = true;
        Debug.Log("In phase 2");
        isInPhase2Transition = false;
    }

    void GroundSlamAttackStart()
    {
        animator.SetTrigger("GroundSlamTrigger");
    }

    //this method is to be called from an animation event on the last frame of the Ground Slam animation
    void GroundSlamProjectile()
    {
        Instantiate(groundSlamProjectilePrefab, groundSlamProjectileSpawnPoint.position, groundSlamProjectileSpawnPoint.rotation);
    }

    void CloseRangeHorizontalSwordAttack()
    {
        animator.SetTrigger("HorizontalSwordAttackTrigger");
        sword.GetComponent<EnemySwordBehaviour>().Attack(true);
    }

    void FarRangeMultipleProjectileAttack()
    {
        
        foreach (Transform spawnPoint in projectileSpawnPoints)
        {
            int randomIndex = Random.Range(0, summoningEnemyPrefabs.Length);
            GameObject selectedEnemyPrefab = summoningEnemyPrefabs[randomIndex];
            Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void FarRangeProjectileBarrageAttack()
    {
        Debug.Log("Projectile Barrage Attack");
    }

    void CloseRangeSummonMinionsAttack()
    {
        animator.SetTrigger("SummoningMinionsTrigger");
        foreach (Transform spawnPoint in summoningSpawnPoints)
        {
            int randomIndex = Random.Range(0, summoningEnemyPrefabs.Length);
            GameObject selectedEnemyPrefab = summoningEnemyPrefabs[randomIndex];
            Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void CloseRangeSwordSpinAttack()
    {
        animator.SetTrigger("SwordSpinAttackTrigger");
        sword.GetComponent<EnemySwordBehaviour>().Attack(true);
    }
}