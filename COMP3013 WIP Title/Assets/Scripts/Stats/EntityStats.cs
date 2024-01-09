
using System;
using UnityEngine;

[Serializable]
public class EntityStats 
{

  public int currentHP = 0;
  public Stats currentStats;

  [SerializeField]
  private Stats defaultStats;

  public void Reset()
  {
    ResetCurrentStats();
    ResetHP();
  }

  public void ResetCurrentStats()
  {
    currentStats = defaultStats;
  }

  public void ResetHP()
  {
    currentHP = currentStats.maxHp;
  }

  public bool IsHpDepleted()
  {
    return currentHP <= 0;
  }

  public void UpdateHP(int hp)
  {

    currentHP += hp;
  
    if(currentHP < 0)
    {
      currentHP = 0;
    }
  }

  public void AddStats(Stats modifier)
  {
    currentStats += modifier;
  }

  public void RemStats(Stats modifier)
  {
    currentStats -= modifier;
  }

}



