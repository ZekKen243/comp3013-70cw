
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardSlot : MonoBehaviour, IDropHandler
{
  public abstract void OnDropCardIcon(CardIcon cardIcon);

  public void Awake()
  {
    InitReferences();
  }

  private void InitReferences()
  {
    cardIcon = GetComponentInChildren<CardIcon>();
    UpdateIcon();
  }

  public void SetCardData(CardItem data)
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

    cardIcon.UpdateIcon();
  }

  public void OnDrop(PointerEventData eventData)
  {
    CardIcon cardIcon = eventData.pointerDrag.GetComponent<CardIcon>();
    OnDropCardIcon(cardIcon);
  }

  public int index 
  {
    get
    {
      return transform.GetSiblingIndex();
    }
  }

  private CardIcon cardIcon = null;
  public CardItem cardData = null;
  public CardSlotType slotType = CardSlotType.NONE;
}

