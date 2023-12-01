

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardData>;

public class EquipmentManager : MonoBehaviour 
{

  public void Awake()
  {
    Instance = this;
    InitReferences();
  }

  public void Start()
  {
    InitTestCards();
  }

  private void InitReferences()
  {
    equipmentWnd = GetComponent<EquipmentWindow>();
  }

  private void InitTestCards()
  {
    SetCard(0, new CardData { id = 3, element = CardElement.ICE });
  }

  public void SwapCards(int srcIndex, int targetIndex)
  {
    CardData srcCard = GetEquipedCard(srcIndex);
    CardData targetCard = GetEquipedCard(targetIndex);

    SetCard(srcIndex, targetCard);
    SetCard(targetIndex, srcCard);
  }

  public void UnequipCard(int index)
  {
    CardData cardData = GetEquipedCard(index);
    if(cardData == null)
    {
      return;
    }

    int invSlotIndex = InventoryManager.Instance.GetEmptySlotIndex();
    if(invSlotIndex < 0)
    {
      Debug.Log("Cannot unequip card, inventory is full.");
      return;
    }

    InventoryManager.Instance.SetCard(invSlotIndex, cardData);
    SetCard(index, null);
  }

  public int GetEmptySlotIndex()
  {
    for (int i = 0; i < Constants.MAX_EQUIPMENT_SIZE; ++i)
    {
      if(GetEquipedCard(i) == null)
      {
        return i;
      }
    }

    return -1;
  }

  public void SetCard(int index, CardData card)
  {
    equipedCards[index] = card;
    equipmentWnd.UpdateSlot(index);
  }

  public CardData GetEquipedCard(int index)
  {
    CardData cardData;

    if (equipedCards.TryGetValue(index, out cardData))
    {
      return cardData;
    } 
    else 
    {
      return null;
    }
  }


  static public EquipmentManager Instance = null;
  public EquipmentWindow equipmentWnd = null;
  private CardCollection equipedCards = new CardCollection();
}

