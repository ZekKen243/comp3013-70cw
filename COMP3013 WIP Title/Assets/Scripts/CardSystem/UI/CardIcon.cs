
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

  public void Awake()
  {
    InitReferences();
  }

  private void InitReferences()
  {
    iconImage = GetComponent<Image>();
    cardSlot = GetComponentInParent<CardSlot>();
    initialParent = transform.parent;
  }

  public void UpdateIcon()
  {
    SetIcon(GetSpriteIconByElement());
  }

  private Sprite GetSpriteIconByElement()
  {
    string spritePath = "cards/default_card_icon";
  
    if(cardData == null)
    {
      return null;
    }

    switch(cardData.element)
    {
      case CardElement.NONE:
        break;

      case CardElement.FIRE:
      {
        spritePath = "cards/fire_card";
        break;
      }

      case CardElement.WIND:
      {
        spritePath = "cards/wind_card";
        break;
      }

      case CardElement.ICE:
      {
        spritePath = "cards/ice_card";
        break;
      }

      default:
        return null;
    }

    return Resources.Load<Sprite>(spritePath);
  }

  public void SetIcon(Sprite icon)
  {
    if(iconImage == null)
    {
      Debug.LogErrorFormat("Can't update the sprite for card icon, no image component found for slot index {0}, slot type {1}", cardSlot.index, slotType);
      return;
    }
    else if(icon == null)
    {
      iconImage.color = new Color(0, 0, 0, 0);
      return;
    }

    iconImage.color = Color.white;
    iconImage.sprite = icon;
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    if(cardData == null)
    {
      return;
    }

    transform.SetParent(transform.root);
    transform.SetAsLastSibling();
    iconImage.raycastTarget = false;

    Debug.LogFormat("Picked a card from slot {0}", slotIndex);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if(cardData == null)
    {
      return;
    }

    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    transform.SetParent(initialParent);
    transform.SetAsFirstSibling();
    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    iconImage.raycastTarget = true;
  }

  // Getters
  public int slotIndex
  {
    get
    {
      return cardSlot.index;
    }
  }

  public CardItem cardData
  {
    get
    {
      return cardSlot.cardData;
    }
  }

  public CardSlotType slotType
  {
    get
    {
      return cardSlot.slotType;
    }
  }

  // Members
  private Transform initialParent = null;
  private Image iconImage = null;
  private CardSlot cardSlot = null;
}

