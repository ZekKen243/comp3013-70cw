
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
  private GameObject attacker = null;

  public void SetAttacker(GameObject gameObject)
  {
    attacker = gameObject;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Enemy"))
    {
      BattleManager.Instance.Attack(attacker, other.gameObject, AttackType.MAGIC);
      Destroy(gameObject);
    }
  }
  
}
