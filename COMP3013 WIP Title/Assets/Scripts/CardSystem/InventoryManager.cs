

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardItem>;

public class InventoryManager : MonoBehaviour 
{

  static public InventoryManager Instance = null;
  public InventoryWindow inventoryWnd = null;
  private CardCollection cardCollection = new CardCollection();


  public void Awake()
  {
    Instance = this;
    InitReferences();
  }

  private void InitReferences()
  {
    inventoryWnd = GetComponent<InventoryWindow>();
  }

  public void Start()
  {
    InitTestCards();
  }

  //! TEST PURPOSE
  private void InitTestCards()
  {
    AutoGiveCard(1);
    AutoGiveCard(2);

  }

  public void EquipCard(int index)
  {
    CardItem cardData = GetCard(index);
    if(cardData == null)
    {
      return;
    }

    int eqSlotIndex = EquipmentManager.Instance.GetEmptySlotIndex();
    if(eqSlotIndex < 0)
    {
      Debug.Log("Cannot unequip card, equipment is full.");
      return;
    }

    EquipmentManager.Instance.SetCard(eqSlotIndex, cardData);
    SetCard(index, null);
  }

  public void SwapCards(int srcIndex, int targetIndex)
  {
    CardItem srcCard = GetCard(srcIndex);
    CardItem targetCard = GetCard(targetIndex);

    SetCard(srcIndex, targetCard);
    SetCard(targetIndex, srcCard);
  }

  public bool AutoGiveCard(int proto_id)
  {
    int index = GetEmptySlotIndex();
    if(index < 0)
    {
      return false;
    }

    CardProtoData protoData = CardProtoManager.Instance.GetCardProto(proto_id);
    if(protoData == null)
    {
      Debug.LogErrorFormat("Cannot aouto give card by card proto id {0}, no proto found.", proto_id);
      return false;
    }

    CardItem cardItem = CardItem.FromProto(index, protoData);
  
    SetCard(index, cardItem);
    return true;
  }

  public bool AutoSetCard(CardItem card)
  {
    int index = GetEmptySlotIndex();
    if(index < 0)
    {
      return false;
    }

    SetCard(index, card);
    return true;
  }

  public void SetCard(int index, CardItem card)
  {
    cardCollection[index] = card;
    inventoryWnd.UpdateSlot(index);
  }

  public CardItem GetCard(int index)
  {
    CardItem cardData;

    if (cardCollection.TryGetValue(index, out cardData))
    {
      return cardData;
    } 
    else 
    {
      return null;
    }
  }

  public int GetEmptySlotIndex()
  {
    for (int i = 0; i < Constants.MAX_INVENTORY_SIZE; i++)
    {
      if(GetCard(i) == null)
      {
        return i;
      }
    }

    return -1;
  }
}

