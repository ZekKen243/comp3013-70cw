

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardData>;

public class InventoryManager : MonoBehaviour 
{
  static public InventoryManager Instance = null;

  public InventoryWindow inventoryWindow = null;

  public void Awake()
  {
    Instance = this;
    inventoryWindow = GetComponent<InventoryWindow>();
  }

  public void Start()
  {
    InitMockCard();
  }


  //! TEST PURPOSE
  private void InitMockCard()
  {

    CardData fireCard = new CardData
    {
      id = 1,
      name = "Fire Card",
      element = CardElement.FIRE
    };

    CardData iceCard = new CardData
    {
      id = 2,
      name = "Ice Card",
      element = CardElement.ICE
    };

    CardData windCard = new CardData
    {
      id = 3,
      name = "Wind Card",
      element = CardElement.WIND
    };

    SetCard(0, fireCard);

    SetCard(2, iceCard);
    SetCard(3, windCard);
  }

  public void EquipCard(int index)
  {
    CardData card = GetCard(index);
    if(card == null)
    {
      return;
    }


    SetCard(index, null);
    EquipmentManager.Instance.SetCard(index, card);
  }

  public void SetCard(int index, CardData card)
  {
    cardCollection[index] = card;
    inventoryWindow.UpdateSlot(index);
  }

  public CardData GetCard(int index)
  {
    CardData cardData;

    if (cardCollection.TryGetValue(index, out cardData))
    {
      return cardData;
    } 
    else 
    {
      return null;
    }
  }

  private CardCollection cardCollection = new CardCollection();
}

