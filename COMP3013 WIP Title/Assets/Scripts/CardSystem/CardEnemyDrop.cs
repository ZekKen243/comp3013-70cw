
using UnityEngine;


public class CardEnemyDrop: MonoBehaviour
{
    public int protoId = 0;
    public GameObject cardPrefab;

    [Range(0.0f, 100.0f)]
    public float dropChance = 100.0f;
    public int dropCount = 1;


    public void DropCard() 
    {
        if(Random.Range(0.0f, 100.0f) <= dropChance)
        {
            //return;
        }

        GameObject droppedItem = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        if(droppedItem == null)
        {
            Debug.LogErrorFormat("Failed to drop card, no card prefab assigned to the CardEnemyDrop script.");
            return;
        }

        CardGameObject card = droppedItem.GetComponent<CardGameObject>();
        if(card == null)
        {
            Destroy(droppedItem);
            Debug.LogErrorFormat("Failed to drop card, the card prefab doesn't have a CardGameObject script.");
            return;
        }

        card.protoId = protoId;
        card.UpdateCardObject();
    }
}



