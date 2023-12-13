using TMPro;
using UnityEngine;


public class InventoryTooltip : MonoBehaviour
{
  private CardItem cardData = null;
  
  public TextMeshProUGUI cardTitle = null;

  public void Open(ref CardItem _cardData)
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

}

