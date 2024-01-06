
using UnityEngine;
using UnityEngine.EventSystems;

class EquipmentSlot : CardSlot, IDropHandler, IPointerClickHandler
{

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

