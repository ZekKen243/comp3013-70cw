using System;
using TMPro;
using UnityEngine;


public class InventoryTooltip : MonoBehaviour
{
  private CardItem cardData = null;
  
  public TextMeshProUGUI cardTitle = null;
  public TextMeshProUGUI cardType = null;
  public TextMeshProUGUI maxHp = null;
  public TextMeshProUGUI swordAttack = null;
  public TextMeshProUGUI magicAttack = null;
  public TextMeshProUGUI moveSpeed = null;
  public TextMeshProUGUI defence = null;
  

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
    cardTitle.text = string.Format("{0}({1})", cardData.name, cardData.protoId);
    cardType.text = string.Format("Type: {0}", cardData.type.ToString());

    // stats
    // todo: Maybe use enums + hash map for flexibility
    maxHp.text = string.Format("Max HP +{0}", cardData.stats.maxHp);
    swordAttack.text = string.Format("Sword Attack +{0}", cardData.stats.swordAttack);
    magicAttack.text = string.Format("Magic Attack +{0}", cardData.stats.magicAttack);
    moveSpeed.text = string.Format("Move Speed +{0}", cardData.stats.speed);
    defence.text = string.Format("Defence +{0}", cardData.stats.defence);
    
  }

}

