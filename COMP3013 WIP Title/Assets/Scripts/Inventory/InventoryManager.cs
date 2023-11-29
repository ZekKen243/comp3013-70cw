

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
    Sprite sprite = Resources.Load<Sprite>("fire_card_sprite");
    if(sprite == null)
    {
      Debug.LogError("Failed to load sprite.");
    }
    CardData card = new CardData
    {
      id = 1,
      name = "Fire Card",
      icon = sprite
    };

    SetCard(0, card);
  }

  public void SetCard(int index, CardData card)
  {
    cardCollection[index] = card;
    inventoryWindow.SetInventoryCard(index, card);
  }

  public CardData GetCard(int index)
  {
    return cardCollection[index];
  }

  private CardCollection cardCollection = new CardCollection();
}

