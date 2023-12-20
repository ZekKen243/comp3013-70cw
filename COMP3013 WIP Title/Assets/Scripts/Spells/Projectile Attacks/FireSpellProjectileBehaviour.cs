using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class FireSpellProjectileBehaviour : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField] 
    private float projectileSpeed;

    [SerializeField] 
    private float maxProjecttileDistance;

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
        if (Vector3.Distance(firingPoint, transform.position) > maxProjecttileDistance)
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
            other.gameObject.GetComponent<EnemyHandler>().TakeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
}
