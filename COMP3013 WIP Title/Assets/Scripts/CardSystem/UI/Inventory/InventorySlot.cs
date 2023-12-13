using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : CardSlot, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    InventoryManager.Instance.SwapCards(cardIcon.slotIndex, index);
  }

  private void OnDropEquipmentCard(CardIcon cardIcon)
  {
    CardItem thisCardData = cardData;
    CardItem dropCardData = cardIcon.cardData;

    InventoryManager.Instance.SetCard(index, dropCardData); 
    EquipmentManager.Instance.SetCard(cardIcon.slotIndex, thisCardData);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if(cardData == null)
    {
      return;
    }

    GetComponentInParent<InventoryWindow>()?.OpenTooltip(cardData);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    GetComponentInParent<InventoryWindow>()?.HideTooltip();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData.button == PointerEventData.InputButton.Right)
    {
      InventoryManager.Instance.EquipCard(index);
    }
  }
}

