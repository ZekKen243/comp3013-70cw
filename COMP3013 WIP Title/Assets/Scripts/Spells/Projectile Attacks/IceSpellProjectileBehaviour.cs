using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class IceSpellProjectileBehaviour : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float maxProjecttileDistance;

    private bool hasHitEnemy;

    public float speedReductionDuration;

    [SerializeField] public int projectileDamage;


    // Start is called before the first frame update
    void Start()
    {
        firingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        if (Vector3.Distance(firingPoint, transform.position) > maxProjecttileDistance && !hasHitEnemy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hasHitEnemy = true;
            other.gameObject.GetComponent<EnemyHandler>().TakeDamage(projectileDamage);
            NavMeshAgent navMeshAgent = other.gameObject.GetComponent<NavMeshAgent>();
            StartCoroutine(TemporarySpeedReduction(navMeshAgent, speedReductionDuration));
            Destroy(this.gameObject.GetComponent<MeshRenderer>());
            Destroy(this.gameObject.GetComponent<SphereCollider>());
            Destroy(this.gameObject.GetComponent<Rigidbody>());
        }
    }

    private IEnumerator TemporarySpeedReduction(NavMeshAgent navMeshAgent, float duration)
    {
        // Store the original speed
        float originalSpeed = navMeshAgent.speed;
        Debug.Log("Original value set. Value:" + originalSpeed);

        // Reduce speed to half
        navMeshAgent.speed /= 2;

        Debug.Log("New value set. Value:" + navMeshAgent.speed);


        yield return new WaitForSeconds(duration);
        Debug.Log("Time passed.");

        navMeshAgent.speed = originalSpeed;
        Destroy(this.gameObject);
    }
}