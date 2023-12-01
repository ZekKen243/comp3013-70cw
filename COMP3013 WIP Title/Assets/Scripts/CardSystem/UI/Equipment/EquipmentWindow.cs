
using UnityEngine;

public class EquipmentWindow : BaseWindow
{
  
  public void Awake()
  {
    InitReferences();
  }
  
  private void InitReferences()
  {
    cardSlots = GetComponentsInChildren<EquipmentSlot>();
  }

  public void UpdateSlot(int index)
  {
    if(cardSlots == null)
    {
      return;
    }

    Debug.LogFormat("Set equipment card index {0}", index);

    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to set equipment card, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].SetCardData(EquipmentManager.Instance.GetEquipedCard(index));
  }


  private EquipmentSlot[] cardSlots = null;
}
