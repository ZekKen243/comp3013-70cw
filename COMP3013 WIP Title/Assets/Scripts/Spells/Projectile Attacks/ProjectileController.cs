
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    public GameObject attacker = null;

    private Vector3 firingPoint;

    [SerializeField] 
    private float projectileSpeed;

    [SerializeField] 
    private float maxProjecttileDistance;

    public int projectileDamage;
    
    void Start()
    {
      firingPoint = transform.position;
    }

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
      if (other.gameObject.tag != "Enemy")
      {
        return;
      }

      BattleManager.Instance.Attack(attacker, other.gameObject, AttackType.MAGIC);
      Destroy(this.gameObject);
    }
}
