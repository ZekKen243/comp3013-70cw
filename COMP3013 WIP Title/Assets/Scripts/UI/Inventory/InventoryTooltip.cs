using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour
{
    private CardData cardData = null;

    public Text cardTitle;

    public void Open(CardData _cardData)
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
