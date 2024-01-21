
using System;
using System.Collections;


[Serializable]
public class CardItem
{
  public int id = 0;
  public int protoId = 0;
  public int count = 1;
  public int currCount = 1;
  public string name = "";
  public int minLevel = 0;
  public CardElement element = CardElement.NONE;
  public CardType type = CardType.NONE;

  public IEnumerator UsageTimer = null;

  public Stats stats = new();

  public bool IsType(CardType cardType)
  {
    return this.type == cardType;
  }

  public bool IsElement(CardElement cardElement)
  {
    return this.element == cardElement;
  }

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

