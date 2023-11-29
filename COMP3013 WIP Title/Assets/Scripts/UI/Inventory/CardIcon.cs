
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class CardIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private Transform initialParent = null;
  private Image iconImage = null;
  private InventorySlot cardSlot = null;

  public void Awake()
  {
    InitReferences();
  }

  private void InitReferences()
  {
    iconImage = GetComponent<Image>();
    cardSlot = GetComponentInParent<InventorySlot>();
    initialParent = transform.parent;
  }

  public CardData GetCardData()
  {
    return cardSlot.cardData;
  }

  public int GetSlotIndex()
  {
    return cardSlot.index;
  }

  public void SetIcon(Sprite icon)
  {
    if(iconImage == null)
    {
      Debug.LogErrorFormat("Can't update the sprite for card icon, no image component found for slot index {0}", cardSlot.index);
      return;
    }

    iconImage.sprite = icon;
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    transform.SetParent(transform.root);
    transform.SetAsLastSibling();
    iconImage.raycastTarget = false;

    Debug.LogFormat("Picked a card from slot {0}", GetSlotIndex());
  }

  public void OnDrag(PointerEventData eventData)
  {
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    transform.SetParent(initialParent);
    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    iconImage.raycastTarget = true;
  }
}

