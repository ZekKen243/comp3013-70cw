


using UnityEngine;
using System;

public class GameEntity : MonoBehaviour 
{
  public event Action<int, int, int> OnTakeDamage;
  public event Action OnDie;


  public EntityStats EntityStatsData = new();

  public Stats stats
  {
    get
    {
      return EntityStatsData.currentStats;
    }
  }

  private void Awake() 
  {
    EntityStatsData.Reset();
  }

  public void TakeDamage(int damage)
  {
    EntityStatsData.IncrementHP(-damage);
    OnTakeDamage?.Invoke(damage, EntityStatsData.currentHP, stats.maxHp);

    if(EntityStatsData.IsHpDepleted())
    {
      Die();
    }
  }

  public void AddCardStats(CardItem cardItem)
  {
    EntityStatsData.AddStats(cardItem.stats);
  }

  public void RemoveCardStats(CardItem cardItem)
  {
    EntityStatsData.RemStats(cardItem.stats);
  }

  public bool IsDead()
  {
    return EntityStatsData.IsHpDepleted();
  }

  private void Die()
  {
    OnDie?.Invoke();
  }

}


