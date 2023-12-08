

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardData>;

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

    CardData fireCard = new CardData
    {
      id = 1,
      name = "Fire Card",
      element = CardElement.FIRE
    };

    CardData iceCard = new CardData
    {
      id = 2,
      name = "Ice Card",
      element = CardElement.ICE
    };

    CardData windCard = new CardData
    {
      id = 3,
      name = "Wind Card",
      element = CardElement.WIND
    };

    SetCard(0, fireCard);
    SetCard(2, iceCard);
    SetCard(3, windCard);
  }

  public void EquipCard(int index)
  {
    CardData cardData = GetCard(index);
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
    CardData srcCard = GetCard(srcIndex);
    CardData targetCard = GetCard(targetIndex);

    SetCard(srcIndex, targetCard);
    SetCard(targetIndex, srcCard);
  }

  public bool AutoSetCard(CardData card)
  {
    int index = GetEmptySlotIndex();
    if(index < 0)
    {
      return false;
    }

    SetCard(index, card);
    return true;
  }

  public void SetCard(int index, CardData card)
  {
    cardCollection[index] = card;
    inventoryWnd.UpdateSlot(index);
  }

  public CardData GetCard(int index)
  {
    CardData cardData;

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

