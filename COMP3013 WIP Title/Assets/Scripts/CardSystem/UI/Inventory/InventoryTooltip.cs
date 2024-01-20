using System;
using TMPro;
using UnityEngine;


public class InventoryTooltip : MonoBehaviour
{

  public static InventoryTooltip Instance = null;
  private CardItem cardData = null;
  
  public TextMeshProUGUI cardTitle = null;
  public TextMeshProUGUI cardType = null;
  public TextMeshProUGUI maxHp = null;
  public TextMeshProUGUI swordAttack = null;
  public TextMeshProUGUI magicAttack = null;
  public TextMeshProUGUI moveSpeed = null;
  public TextMeshProUGUI defence = null;
  public TextMeshProUGUI usage = null;

  void Awake()
  {
    Instance = this;
  }

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
    // todo: Maybe use enums + hash map for flexibility. It might require the whole stats system to be changed
    maxHp.text = string.Format("Max HP +{0}", cardData.stats.maxHp);
    swordAttack.text = string.Format("Sword Attack +{0}", cardData.stats.swordAttack);
    magicAttack.text = string.Format("Magic Attack +{0}", cardData.stats.magicAttack);
    moveSpeed.text = string.Format("Move Speed +{0}", cardData.stats.moveSpeed);
    defence.text = string.Format("Defence +{0}", cardData.stats.defence);
    usage.text = string.Format("Remaining {0}/{1}", cardData.currCount, cardData.count);
  }

}

