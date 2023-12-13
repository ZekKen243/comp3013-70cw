
using UnityEngine;

public class InventoryWindow : BaseWindow
{

  public void Awake()
  {
    InitReferences();
  }

  public void Start()
  {
    RefreshInventory();
  }

  private void InitReferences()
  {
    cardSlots = GetComponentsInChildren<InventorySlot>();
    inventoryTooltip = GetComponentInChildren<InventoryTooltip>(true);
  }

  public void RefreshInventory()
  {
    for(int i = 0; i < cardSlots.Length; ++i)
    {
      UpdateSlot(i);
    }
  }

  public void UpdateSlot(int index)
  {
    Debug.LogFormat("Update inventory slot {0}", index);

    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to update inventory slot, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].SetCardData(InventoryManager.Instance.GetCard(index));
  }

  public void OpenTooltip(CardItem cardData)
  {
    if(inventoryTooltip == null)
    {
      Debug.LogErrorFormat("Cannot open tooltip, no child with InventoryTooltip script found.");
      return;
    }

    inventoryTooltip.Open(ref cardData);
  }

  public void HideTooltip()
  {
    inventoryTooltip.Hide();
  }

  private InventorySlot[] cardSlots = null;
  private InventoryTooltip inventoryTooltip = null;
  
}
