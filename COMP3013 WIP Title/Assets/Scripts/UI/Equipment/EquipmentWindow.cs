
using UnityEngine;

public class EquipmentWindow : BaseWindow
{

  private EquipmentSlot[] cardSlots = null;
  
  public void Awake()
  {
    InitReferences();
  }
  
  private void InitReferences()
  {
    cardSlots = GetComponentsInChildren<EquipmentSlot>();
  }

  public void SetEquipmentCard(int index, CardData cardData)
  {
    Debug.LogFormat("Set equipment card index {0}, data {1}", index, cardData);

    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to set equipment card, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].UpdateSlot();
  }
}
