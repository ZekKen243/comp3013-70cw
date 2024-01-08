
using System;


[Serializable]
public class Stats 
{
  public int magicAttack = 0;
  public int swordAttack = 0;
  public int maxHp = 0;
  public int speed = 0;
  public int defence = 0;


  public static Stats operator +(Stats stats1, Stats stats2)
  {
    Stats result = new()
    {
      magicAttack = stats1.magicAttack + stats2.magicAttack,
      swordAttack = stats1.swordAttack + stats2.swordAttack,
      maxHp = stats1.maxHp + stats2.maxHp,
      speed = stats1.speed + stats2.speed,
      defence = stats1.defence + stats2.defence
    };

    return result;
  }

  public static Stats operator -(Stats stats1, Stats stats2)
  {
    Stats result = new()
    {
      magicAttack = stats1.magicAttack - stats2.magicAttack,
      swordAttack = stats1.swordAttack - stats2.swordAttack,
      maxHp = stats1.maxHp - stats2.maxHp,
      speed = stats1.speed - stats2.speed,
      defence = stats1.defence - stats2.defence
    };

    return result;
  }
}
