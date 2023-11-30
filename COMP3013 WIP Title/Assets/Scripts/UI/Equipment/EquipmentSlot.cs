
using UnityEngine;
using UnityEngine.EventSystems;

// todo: make an abstract class that will be inherited
class EquipmentSlot : MonoBehaviour, IDropHandler
{
  public EquipmentIcon cardIcon = null;
  public CardData cardData = null;


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
    cardIcon = GetComponentInChildren<EquipmentIcon>();
  }

  public void UpdateSlot()
  {
    SetCardData(EquipmentManager.Instance.GetEquipedCard(index));
  }

  public void SetCardData(CardData _cardData)
  {
    cardData = _cardData;
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

    CardData dropCardData = cardIcon.GetCardData();
    CardData thisCardData = cardData;

    EquipmentManager.Instance.SetCard(index, dropCardData);
    InventoryManager.Instance.SetCard(cardIcon.GetSlotIndex(), thisCardData);
    return true;
  }

  private bool OnDropEquipmentCard(EquipmentIcon cardIcon)
  {
    if(cardIcon == null)
    {
      return false;
    }

    CardData dropCardData = cardIcon.GetCardData();
    CardData thisCardData = cardData;

    EquipmentManager.Instance.SetCard(index, dropCardData);
    EquipmentManager.Instance.SetCard(cardIcon.GetSlotIndex(), thisCardData);
    return true;
  }

}

