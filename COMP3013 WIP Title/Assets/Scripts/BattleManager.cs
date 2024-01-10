

using UnityEngine;

class BattleManager
{
  static private BattleManager instance = null;

  public static BattleManager Instance
  {
    get
    {
      if(instance == null)
      {
        instance = new();
      }

      return instance;
    }
  }

  public void Attack(GameObject attacker, GameObject target, AttackType attackType)
  {
    GameEntity attackerEntity = attacker.GetComponent<GameEntity>();
    GameEntity targetEntity = target.GetComponent<GameEntity>();

    if(attackerEntity == null || targetEntity == null)
    {
      Debug.LogErrorFormat("Cannot process attack, no GameEntity script found. Attacker {0}, Target: {1}", attacker.name, target.name);
      return;
    }

    int damage = CalculateDamage(attackerEntity.stats, targetEntity.stats, attackType);

    // Damage could end up negative if the defence modifier is higher than the attack value
    // If the damage is negative, the targets hp will increase
    if(damage < 0)
    {
      damage = 0;
    }

    targetEntity.TakeDamage(damage);
  }

  private int CalculateDamage(Stats attackerStats, Stats targetStats, AttackType attackType)
  {
    switch(attackType)
    {
      case AttackType.SWORD:
      {
        return CalculateSwordDamage(attackerStats.swordAttack, targetStats);
      }
      case AttackType.MAGIC:
      {
         return CalculateMagicDamage(attackerStats.magicAttack, targetStats);
      }
      default:
        break;
    }

    return 0;
  }


  // Calculate Sword damage based on target's stats
  private int CalculateSwordDamage(int attackerDamage, Stats targetStats)
  {
    int damage = attackerDamage;
    damage -= targetStats.defence;

    return damage;
  }

  // Calculate Magic damage based on target's stats
  // Magic Attack could have different calculation from a sword attack
  private int CalculateMagicDamage(int attackerDamage, Stats targetStats)
  {
    int damage = attackerDamage;
    damage -= targetStats.defence;

    return damage;
  }

}




