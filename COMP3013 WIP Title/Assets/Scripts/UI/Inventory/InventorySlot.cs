
using UnityEngine;
using UnityEngine.EventSystems;

// todo: make an abstract class that will be inherited
class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
  public InventoryIcon cardIcon = null;
  public CardData cardData = null;
  private InventoryWindow inventoryWindow = null;


  public int index 
  {
    get
    {
      return transform.GetSiblingIndex();
    }
  }

  public void Awake()
  {
    InitReferences();
  }

  private void InitReferences()
  {
    cardIcon = GetComponentInChildren<InventoryIcon>();
    inventoryWindow = GetComponentInParent<InventoryWindow>();
  }

  public void UpdateSlot()
  {
    SetCardData(InventoryManager.Instance.GetCard(index));
  }

  public void SetCardData(CardData data)
  {
    cardData = data;
    UpdateIcon();
  }

  private void UpdateIcon()
  {
    if(cardIcon == null)
    {
      Debug.LogErrorFormat("Card slot with index {0} doesn't have a child with CardIcon script.", index);
      return;
    }

   if(cardData == null)
   {
    cardIcon.SetIcon(null);
    return;
   }

    cardIcon.SetIcon(GetSpriteByElement());
  }

  private Sprite GetSpriteByElement()
  {
    if(cardData == null)
    {
      return null;
    }

    string spritePath = "";

    switch(cardData.element)
    {
      case CardElement.NONE:
      {
        return null;
      }
      
      case CardElement.FIRE:
      {
        spritePath = "fire_card_sprite";
        break;
      }

      case CardElement.WIND:
      {
        spritePath = "wind_card_sprite";
        break;
      }

      case CardElement.ICE:
      {
        spritePath = "ice_card_sprite";
        break;
      }

      default:
        break;
    }

    return Resources.Load<Sprite>(spritePath);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if(cardData == null)
    {
      return;
    }

    inventoryWindow.OpenTooltip(cardData);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    inventoryWindow.HideTooltip();
  }

  public void OnDrop(PointerEventData eventData)
  {
    InventoryIcon invCardIcon = eventData.pointerDrag.GetComponent<InventoryIcon>();
    if(OnDropInventoryCard(invCardIcon))
    {
      return;
    }

    EquipmentIcon eqCardIcon = eventData.pointerDrag.GetComponent<EquipmentIcon>();
    OnDropEquipmentCard(eqCardIcon);
  }

  private bool OnDropInventoryCard(InventoryIcon cardIcon)
  {
    if(cardIcon == null)
    {
      return false;
    }
    else if(index == cardIcon.GetSlotIndex())
    {
      return false;
    }

    CardData dropCardData = cardIcon.GetCardData();
    CardData thisCardData = cardData;

    InventoryManager.Instance.SetCard(index, dropCardData);
    InventoryManager.Instance.SetCard(cardIcon.GetSlotIndex(), thisCardData);
    return true;
  }

  private bool OnDropEquipmentCard(EquipmentIcon cardIcon)
  {
    if(cardIcon == null)
    {
      return false;
    }
    else if(index == cardIcon.GetSlotIndex())
    {
      return false;
    }

    CardData dropCardData = cardIcon.GetCardData();
    CardData thisCardData = cardData;

    InventoryManager.Instance.SetCard(index, dropCardData);
    EquipmentManager.Instance.SetCard(cardIcon.GetSlotIndex(), thisCardData);
    return true;
  }
}

