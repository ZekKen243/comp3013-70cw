
using UnityEngine;

public class InventoryWindow : BaseWindow
{

  private InventorySlot[] cardSlots = null;
  private InventoryTooltip inventoryTooltip = null;
  
  public void Awake()
  {
    InitReferences();
  }
  
  private void InitReferences()
  {
    cardSlots = GetComponentsInChildren<InventorySlot>();
    inventoryTooltip = GetComponentInChildren<InventoryTooltip>(true);
  }


  public void SetInventoryCard(int index, CardData cardData)
  {
    Debug.LogFormat("Set inventory card index {0}, data {1}", index, cardData);

    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to set inventory card, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].SetCardData(cardData);
  }

  public void OpenTooltip(CardData cardData)
  {
    if(inventoryTooltip == null)
    {
      Debug.LogErrorFormat("Cannot open tooltip, no child with InventoryTooltip script found.");
      return;
    }
    inventoryTooltip.Open(cardData);
  }

  public void HideTooltip()
  {
    inventoryTooltip.Hide();
  }
}
