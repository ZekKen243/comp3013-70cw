
using System;
using UnityEngine;
using Utils;

using CardProtoDict = System.Collections.Generic.SortedDictionary<int, CardProtoData>;

[Serializable]
public class CardProtoData
{
  public int proto_id;
  public string name;
  public int min_level;
  public string element;

  // todo: Add attributes
  // ex:
  // public CardAttribute[] cardAttributes;
  // CardAttribute => a class with type => value
  // Example: CardAttribute(AttributeType.ATTACK, 2)

  public CardElement elementEnum
  {
    get
    {
      return EnumUtil.ParseEnum<CardElement>(element);
    }
    set
    {
      element = value.ToString();
    }
  }
}


public class CardProtoManager
{
  private CardProtoDict cardProtoTable = new();
  private static CardProtoManager instance = null;

  static public CardProtoManager Instance
  {
    get
    {
      if(instance == null)
      {
        instance = new CardProtoManager();
      }

      return instance;
    }
  }

  public void Initialise()
  {
    ReadProtoTable();
  }

  private void ReadProtoTable()
  {
    Debug.Log("Loading card proto table.");
    string jsonFilePath = Application.dataPath + "/Resources/CardProtoTable.json";

    string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
    CardProtoData[] cardProtoArr = JsonHelper.FromJson<CardProtoData>(jsonContent);

    foreach(CardProtoData cardProto in cardProtoArr)
    {
      Debug.LogFormat("Load card proto {0}", cardProto.proto_id);
      AddCardProto(cardProto);
    }

    Debug.LogFormat("Card proto table loaded {0} proto cards.", GetProtoTableSize());
  }

  public void Clear()
  {
    cardProtoTable.Clear();
  }

  public int GetProtoTableSize()
  {
    return cardProtoTable.Count;
  }

  public void AddCardProto(CardProtoData cardProto)
  {
    int protoID = cardProto.proto_id;

    if(cardProtoTable.ContainsKey(protoID))
    {
      Debug.LogWarningFormat("Card proto with id {0} duplicated, data overridden.", protoID);
    }
    cardProtoTable[protoID] = cardProto;
  }

  public CardProtoData GetCardProto(int protoID)
  {
    CardProtoData protoData;
    if(cardProtoTable.TryGetValue(protoID, out protoData))
    {
      return protoData;
    }

    Debug.LogWarningFormat("Card proto data not found for id: {0}", protoID);
    return null;
  }

}



