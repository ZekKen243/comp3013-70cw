
using UnityEngine;
using UnityEngine.EventSystems;

class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
  public CardIcon cardIcon = null;
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
    cardIcon = GetComponentInChildren<CardIcon>();
    inventoryWindow = GetComponentInParent<InventoryWindow>();
  }


  public void SetCardData(CardData _cardData)
  {
    Debug.LogFormat("Set card data for card slot {0}.", index);
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

    cardIcon.SetIcon(cardData.icon);
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
    CardIcon cardIcon = eventData.pointerDrag.GetComponent<CardIcon>();
    if(cardIcon == null)
    {
      return;
    }
    else if(index == cardIcon.GetSlotIndex())
    {
      return;
    }


    CardData cardData = cardIcon.GetCardData();
    InventoryManager.Instance.SetCard(index, cardData);
    InventoryManager.Instance.SetCard(cardIcon.GetSlotIndex(), null);
  }
}

