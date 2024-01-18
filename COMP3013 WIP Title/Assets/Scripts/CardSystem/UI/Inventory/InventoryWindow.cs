
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

  public void OnEnable()
  {
    HideTooltip();
  }

  private void InitReferences()
  {
    cardSlots = GetComponentsInChildren<InventorySlot>();
    inventoryTooltip = GetComponentInChildren<InventoryTooltip>(true);
    inventoryPlayerStats = GetComponentInChildren<InventoryPlayerStats>(true);
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

    inventoryPlayerStats.Hide();
    inventoryTooltip.Open(ref cardData);
  }

  public void HideTooltip()
  {
    inventoryTooltip.Hide();
    inventoryPlayerStats.Open();
  }

  private InventorySlot[] cardSlots = null;
  private InventoryTooltip inventoryTooltip = null;
  private InventoryPlayerStats inventoryPlayerStats = null;
  
}
