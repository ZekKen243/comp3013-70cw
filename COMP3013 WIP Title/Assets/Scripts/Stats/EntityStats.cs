
using System;
using UnityEngine;

[Serializable]
public class EntityStats 
{
  public event Action<EntityStats> OnStatsUpdate;

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
    OnStatsUpdate?.Invoke(this);
  }

  public void ResetHP()
  {
    currentHP = currentStats.maxHp;
    OnStatsUpdate?.Invoke(this);
  }

  public bool IsHpDepleted()
  {
    return currentHP <= 0;
  }

  public void IncrementHP(int hp)
  {

    currentHP += hp;
  
    if(currentHP < 0)
    {
      currentHP = 0;
    }

    OnStatsUpdate?.Invoke(this);
  }

  public void AddStats(Stats modifier)
  {
    currentStats += modifier;
    OnStatsUpdate?.Invoke(this);
  }

  public void RemStats(Stats modifier)
  {
    currentStats -= modifier;
    OnStatsUpdate?.Invoke(this);
  }

}



