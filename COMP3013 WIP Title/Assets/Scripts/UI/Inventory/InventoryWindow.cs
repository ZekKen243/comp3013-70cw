
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

  public void UpdateSlot(int index)
  {
    Debug.LogFormat("Update inventory slot {0}", index);

    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to update inventory slot, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].UpdateSlot();
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
