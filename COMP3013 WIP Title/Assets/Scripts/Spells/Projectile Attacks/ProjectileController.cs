
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
  private GameObject attacker = null;

  public void SetAttacker(GameObject gameObject)
  {
    attacker = gameObject;
  }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      BattleManager.Instance.Attack(attacker, other.gameObject, AttackType.MAGIC);
      Destroy(gameObject);
    }
  }
  
}
