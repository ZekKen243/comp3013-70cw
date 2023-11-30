

using UnityEngine;

using CardCollection = System.Collections.Generic.SortedDictionary<int, CardData>;

public class EquipmentManager : MonoBehaviour 
{
  static public EquipmentManager Instance = null;

  public EquipmentWindow equipmentWnd = null;

  public void Awake()
  {
    Instance = this;
    equipmentWnd = GetComponent<EquipmentWindow>();
  }

  public void SetCard(int index, CardData card)
  {
    equipedCards[index] = card;
    equipmentWnd.SetEquipmentCard(index, card);
  }

  public int GetFreeCellIndex()
  {
    return 0;
  }

  public CardData GetEquipedCard(int index)
  {
    return equipedCards[index];
  }

  private CardCollection equipedCards = new CardCollection();
}

