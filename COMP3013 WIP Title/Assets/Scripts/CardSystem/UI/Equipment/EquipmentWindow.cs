
using UnityEngine;

public class EquipmentWindow : BaseWindow
{
  
  public void Awake()
  {
    InitReferences();
    RefreshEequipment();
  }

  public void Start()
  {

  }
  
  public void RefreshEequipment()
  {
    for(int i = 0; i < cardSlots.Length; ++i)
    {
      UpdateSlot(i);
    }
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


    if(index >= cardSlots.Length || index < 0)
    {
      Debug.LogErrorFormat("Failed to set equipment card, index out of range. Index: {0}, slots available: {1}", index, cardSlots.Length);
      return;
    }

    cardSlots[index].SetCardData(EquipmentManager.Instance.GetEquipedCard(index));
  }


  private EquipmentSlot[] cardSlots = null;
}
