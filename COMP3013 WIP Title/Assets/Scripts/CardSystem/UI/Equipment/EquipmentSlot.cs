
using UnityEngine;
using UnityEngine.EventSystems;

class EquipmentSlot : CardSlot, IDropHandler, IPointerClickHandler
{
  private Keystroke keystroke = null;
  private CardUsageBar cardUsageBar = null;

  void Start()
  {
    keystroke = GetComponentInChildren<Keystroke>();
    cardUsageBar = GetComponentInChildren<CardUsageBar>();
  }

  public override void OnUpdateCardSlot(CardItem cardItem)
  {
    if(keystroke == null)
    {
      Debug.LogError("No keystroke script found in the children of an equipment slot.");
      return;
    }

    if(cardItem == null)
    {
      cardUsageBar.progress = 0;
      return;
    }

    if(cardItem.currCount > 0 && cardItem.count > 0)
    {
      cardUsageBar.progress = (float) cardItem.currCount / (float) cardItem.count;
      Debug.LogFormat("AAAA {0}/{1}, PERC {2}", cardItem.currCount, cardItem.count, cardUsageBar.progress);
    }
    
    //keystroke.Hide();
  }

  public override void OnDropCardIcon(CardIcon cardIcon)
  {
    switch(cardIcon.slotType)
    {
      case CardSlotType.INVENTORY:
      {
        OnDropInventoryCard(cardIcon);
        break;
      }
      case CardSlotType.EQUIPMENT:
      {
        OnDropEquipmentCard(cardIcon);
        break;
      }

      default:
        break;
    }
  }

  private void OnDropInventoryCard(CardIcon cardIcon)
  {
    if(cardIcon == null)
    {
      return;
    }

    CardItem dropCardData = cardIcon.cardData;
    CardItem thisCardData = cardData;

    EquipmentManager.Instance.SetCard(index, dropCardData);
    InventoryManager.Instance.SetCard(cardIcon.slotIndex, thisCardData);
  }

  private void OnDropEquipmentCard(CardIcon cardIcon)
  {
    if(cardIcon == null)
    {
      return;
    }

    EquipmentManager.Instance.SwapCards(cardIcon.slotIndex, index);
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right)
    {
      EquipmentManager.Instance.UnequipCard(index);
    }
  }

}

