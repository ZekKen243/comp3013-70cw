using TMPro;
using UnityEngine;


public class InventoryPlayerStats : MonoBehaviour
{
  private GameEntity playerEntity = null;
  public TextMeshProUGUI maxHp = null;
  public TextMeshProUGUI swordAttack = null;
  public TextMeshProUGUI magicAttack = null;
  public TextMeshProUGUI moveSpeed = null;
  public TextMeshProUGUI defence = null;
  
  
  private void Start()
  {
    InitReferences();
    UpdateText(playerEntity.EntityStatsData);
  }

  private void InitReferences()
  {
    GameObject player = GameObject.FindWithTag("Player");

    if(player == null)
    {
      return;
    }

    playerEntity = player.GetComponent<GameEntity>();
    playerEntity.EntityStatsData.OnStatsUpdate += OnStatsUpdate;
  }
  
  public void OnStatsUpdate(EntityStats entityStats)
  {
    UpdateText(entityStats);
  }

  public void Open()
  {
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }

  public void UpdateText(EntityStats entityStats)
  {
    // stats
    // todo: Maybe use enums + hash map for flexibility. It might require the whole stats system to be changed
    maxHp.text = string.Format("HP: {0}/{1}", entityStats.currentHP, entityStats.currentStats.maxHp);
    swordAttack.text = string.Format("Sword Attack: {0}", entityStats.currentStats.swordAttack);
    magicAttack.text = string.Format("Magic Attack: {0}", entityStats.currentStats.magicAttack);
    moveSpeed.text = string.Format("Move Speed: {0}", entityStats.currentStats.moveSpeed);
    defence.text = string.Format("Defence: {0}", entityStats.currentStats.defence);
    
  }

}

