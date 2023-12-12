
using UnityEngine;

public class CardGameObject : MonoBehaviour
{

  public bool canPickUp = true;
  public bool animate = true;
  
  [Range(0.0f, 300.0f), ]
  public float rotationSpeed = 30.0f;

  private CardData cardData;

  public void Awake()
  {
    cardData = new CardData() { id = transform.GetSiblingIndex() + 20, element = CardElement.FIRE, level = 2, name = "Wind Card" };
    UpdateCardObject();
  }

  private void UpdateCardObject()
  {
    Renderer renderer = GetComponent<Renderer>();
    switch(cardData.element)
    {
      case CardElement.FIRE:
      {
        renderer.material.color = Color.red;
        break;
      }
      case CardElement.WIND:
      {
        renderer.material.color = Color.green;
        break;
      }

      default:
        break;
    }
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
