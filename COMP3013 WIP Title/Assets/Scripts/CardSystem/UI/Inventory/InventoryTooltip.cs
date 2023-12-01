using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour
{
  public void Open(ref CardData _cardData)
  {
    cardData = _cardData;
    UpdateText();
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    cardData = null;
    gameObject.SetActive(false);
  }

  private void UpdateText()
  {
    cardTitle.text = cardData.name;
  }


  private CardData cardData = null;
  public Text cardTitle = null;
}

