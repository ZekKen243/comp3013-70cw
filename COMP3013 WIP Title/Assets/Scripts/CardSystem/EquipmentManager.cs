

using UnityEngine;
using System.Collections;
using CardCollection = System.Collections.Generic.SortedDictionary<int, CardItem>;

public class EquipmentManager : MonoBehaviour 
{

  static public EquipmentManager Instance = null;
  public EquipmentWindow equipmentWnd = null;
  private CardCollection equipedCards = new();

  private GameObject player = null;

  private void Start() 
  {
    RefreshPlayerStats();
  }

  private void Awake()
  {
    Instance = this;
    InitReferences();
  }


  private void InitReferences()
  {
    equipmentWnd = GetComponent<EquipmentWindow>();
    player = GameObject.FindWithTag("Player");
  }

  private void OnUnequipCard(int index, CardItem cardItem)
  {
    if(cardItem.IsType(CardType.PASSIVE))
    {
      StopCoroutine(cardItem.UsageTimer);
    }
  }

  private void OnEquipCard(int index, CardItem cardItem)
  {
    if(cardItem.IsType(CardType.PASSIVE))
    {
      cardItem.UsageTimer = PassiveCardUsageTimer(index, cardItem);
      StartCoroutine(cardItem.UsageTimer);
    }
  }

  private IEnumerator PassiveCardUsageTimer(int index, CardItem cardItem)
  {
    if(!cardItem.IsType(CardType.PASSIVE))
    {
      Debug.LogError("Cannot set usage timer for a non passive card.");
      yield break;
    }

    while(true)
    {
      yield return new WaitForSeconds(1);

      UpdateSlot(index);
      if(--cardItem.currCount <= 0)
      {
        SetCard(index, null);
        yield break;
      }
      
    }
  }

  public void RefreshPlayerStats()
  {
    if(!player)
    {
      Debug.LogErrorFormat("Failed to refresh player stats, no player found.");
      return;
    }

    GameEntity playerEntity = player.GetComponent<GameEntity>();
    if(!playerEntity)
    {
      Debug.LogErrorFormat("Failed to refresh player stats, player has no CharacterStats script.");
      return;
    }

    playerEntity.EntityStatsData.ResetCurrentStats();


    foreach(var pair in equipedCards)
    {
      if(pair.Value == null)
      {
        continue;
      }

      playerEntity.AddCardStats(pair.Value);
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

  public void UpdateSlot(int index)
  {
    equipmentWnd.UpdateSlot(index);
  }

  public void SetCard(int index, CardItem card)
  {
    if(card != null)
    {
      OnEquipCard(index, card);
    }

    CardItem equipedCard = GetEquipedCard(index);
    if(equipedCard != null)
    {
      OnUnequipCard(index, equipedCard);
    }

    equipedCards[index] = card;
    equipmentWnd.UpdateSlot(index);
    RefreshPlayerStats();
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

