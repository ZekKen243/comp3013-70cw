

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardItem>;

public class EquipmentManager : MonoBehaviour 
{

  static public EquipmentManager Instance = null;
  public EquipmentWindow equipmentWnd = null;
  private CardCollection equipedCards = new();

  private GameObject player = null;

  public void Awake()
  {
    Instance = this;
    InitReferences();
  }


  private void InitReferences()
  {
    equipmentWnd = GetComponent<EquipmentWindow>();
    player = GameObject.FindWithTag("Player");
  }

  public void RefreshPlayerStats()
  {
    if(!player)
    {
      Debug.LogErrorFormat("Failed to refresh player stats, no player found.");
      return;
    }

    CharacterStats charStats = player.GetComponent<CharacterStats>();
    if(!charStats)
    {
      Debug.LogErrorFormat("Failed to refresh player stats, player has no CharacterStats script.");
      return;
    }

    charStats.ResetStats();

    foreach(var pair in equipedCards)
    {
      if(pair.Value == null)
      {
        continue;
      }
      charStats.AddCardStats(pair.Value);
    }
  }

  public void SwapCards(int srcIndex, int targetIndex)
  {
    CardItem srcCard = GetEquipedCard(srcIndex);
    CardItem targetCard = GetEquipedCard(targetIndex);

    SetCard(srcIndex, targetCard);
    SetCard(targetIndex, srcCard);
  }

  public void UnequipCard(int index)
  {
    CardItem cardData = GetEquipedCard(index);
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

  public void SetCard(int index, CardItem card)
  {
    RefreshPlayerStats();
    equipedCards[index] = card;
    equipmentWnd.UpdateSlot(index);
  }

  public CardItem GetEquipedCard(int index)
  {
    CardItem cardData;

    if (equipedCards.TryGetValue(index, out cardData))
    {
      return cardData;
    } 
    else 
    {
      return null;
    }
  }

}

