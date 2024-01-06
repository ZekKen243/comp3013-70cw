
using System;


[Serializable]
public class CardItemStats {
  public int magicAttack = 0;
  public int swordAttack = 0;
  public int maxHp = 0;
  public int speed = 0;
  public int defence = 0;

}

[Serializable]
public class CardItem
{
  public int id = 0;
  public int protoId = 0;
  public int count = 1;
  public string name = "";
  public int minLevel = 0;
  public CardElement element = CardElement.NONE;
  public CardType type = CardType.NONE;

  public CardItemStats stats = new();

  // todo: Add attributes
  // ex:
  // public CardAttribute[] cardAttributes;
  // CardAttribute => a class with type => value
  // Example: CardAttribute(AttributeType.ATTACK, 2)

  public static CardItem FromProto(int id, CardProtoData protoData)
  {
    return new() {
      id = id,
      protoId = protoData.proto_id,
      name = protoData.name,
      minLevel = protoData.min_level,
      element = protoData.elementEnum,
      type = protoData.typeEnum,
      stats = protoData.stats
    };
  }
}

