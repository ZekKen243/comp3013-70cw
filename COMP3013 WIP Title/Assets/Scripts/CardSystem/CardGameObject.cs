
using UnityEngine;

public class CardGameObject : MonoBehaviour
{

  [Range(0.0f, 100.0f)]
  public float rotationSpeed = 30.0f;


  public bool animate = true;
  public bool canPickUp = true;

  private CardData cardData;

  public void Awake()
  {
    cardData = new CardData() { id = transform.GetSiblingIndex() + 20, element = CardElement.WIND, level = 2, name = "Wind Card" };
  }

  public void Update()
  {
    UpdateAnimation();
  }

  private void UpdateAnimation()
  {
    if(!animate)
    {
      return;
    }

    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
  }

  public void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player")
    {
      PickUp();
    }
  }

  private void PickUp()
  {
    if(!canPickUp || !InventoryManager.Instance.AutoSetCard(cardData))
    {
      return;
    }

    Destroy(gameObject);
  }
}
