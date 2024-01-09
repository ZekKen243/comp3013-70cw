
using System;


[Serializable]
public class Stats 
{
  public int magicAttack = 0;
  public int swordAttack = 0;
  public int maxHp = 0;
  public int moveSpeed = 0;
  public int defence = 0;


  public static Stats operator +(Stats stats1, Stats stats2)
  {
    Stats result = new()
    {
      magicAttack = stats1.magicAttack + stats2.magicAttack,
      swordAttack = stats1.swordAttack + stats2.swordAttack,
      maxHp = stats1.maxHp + stats2.maxHp,
      moveSpeed = stats1.moveSpeed + stats2.moveSpeed,
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
      moveSpeed = stats1.moveSpeed - stats2.moveSpeed,
      defence = stats1.defence - stats2.defence
    };

    return result;
  }
}
