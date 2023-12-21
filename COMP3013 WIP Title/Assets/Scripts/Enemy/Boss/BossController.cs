using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float bossHealth = 300f;
    public float phaseThreshold = 150f; //health required for boss to transition into phase 2
    public float delayBeforePhase2 = 2f; //allows for phase transition animation, adjust as needed
    public float attackRange = 5f; //distance at which close range attacks will trigger
    public float minCooldownTime = 3f;
    public float maxCooldownTime = 6f;

    private bool isPhase2 = false;
    private float attackCooldown = 0f;

    // Define attack methods for each phase
    private System.Action[] phase1CloseAttacks;
    private System.Action[] phase2CloseAttacks;

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("Player").transform; // Adjust accordingly
        agent = GetComponent<NavMeshAgent>();

        //initialize close range attack methods for each phase
        phase1CloseAttacks = new System.Action[]
        {
            CloseRangeGroundSlamAttack,
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
        if (bossHealth <= phaseThreshold)
        {
            if (!isPhase2)
            {
                StartCoroutine(Phase2Routine());
            }
            else
            {
                //in Phase 2, loop through attacks based on distance to player
                if (attackCooldown <= 0f)
                {
                    if (Vector3.Distance(transform.position, player.position) > attackRange)
                    {
                        //only one long range attack in phase 2, so no need to randomly iterate through array
                        FarRangeMultipleProjectileAttack();
                    }
                    else
                    {
                        //randomly select a close-range attack in Phase 2
                        int randomIndex = Random.Range(0, phase2CloseAttacks.Length);
                        phase2CloseAttacks[randomIndex]?.Invoke();
                    }

                    //set the cooldown
                    attackCooldown = Random.Range(minCooldownTime, maxCooldownTime);
                }

                //update cooldown
                attackCooldown = Mathf.Max(0f, attackCooldown - Time.deltaTime);
            }
        }
        else
        {
            //in Phase 1, follow the player and loop through attacks based on distance
            agent.SetDestination(player.position);

            if (attackCooldown <= 0f)
            {
                if (Vector3.Distance(transform.position, player.position) > attackRange)
                {
                    //only one long range attack in phase 2, so no need to randomly iterate through array
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
        yield return new WaitForSeconds(delayBeforePhase2);
        //trigger phase transition animation
        isPhase2 = true;
    }

    void CloseRangeGroundSlamAttack()
    {

    }

    void CloseRangeHorizontalSwordAttack()
    {

    }

    void FarRangeMultipleProjectileAttack()
    {

    }

    void FarRangeProjectileBarrageAttack()
    {

    }

    void CloseRangeSummonMinionsAttack()
    {

    }

    void CloseRangeSwordSpinAttack()
    {

    }

    void FarRangeAnotherLongRangeAttack()
    {

    }
}