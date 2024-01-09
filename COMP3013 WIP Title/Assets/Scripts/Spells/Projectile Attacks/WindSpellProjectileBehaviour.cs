
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class WindSpellProjectileBehaviour : MonoBehaviour
{
    public GameObject attacker = null;

    private Vector3 firingPoint;

    public float projectileSpeed;
 
    public float maxProjecttileDistance;

    public int projectileDamage;
    public float pushBackSpeed;


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
            Vector3 movement = -Vector3.forward;  // Opposite direction compared to previous example
            other.gameObject.transform.Translate(movement * pushBackSpeed * Time.deltaTime);
            Debug.Log("Applied force. Force: ");
            Destroy(this.gameObject);
        }
    }
}
