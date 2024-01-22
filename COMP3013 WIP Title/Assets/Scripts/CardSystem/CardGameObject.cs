
using UnityEngine;
using UnityEngine.Audio;

public class CardGameObject : MonoBehaviour
{

  public int protoId = 0;

  [Range(1, 500)]
  public int count = 1;
  public bool canPickUp = true;
  public bool animate = true;
  
  [Range(0.0f, 300.0f), ]
  public float rotationSpeed = 30.0f;

  public AudioSource audioSource = null;

  public void Awake()
  {
    // ! dev purpose 
    protoId = Random.Range(0, 5);
    UpdateCardObject();
  }

  public void UpdateCardObject()
  {
    Renderer renderer = GetComponent<Renderer>();
    Light light = GetComponentInChildren<Light>();
    
    CardProtoData protoData = CardProtoManager.Instance.GetCardProto(protoId);

    if(renderer == null || light == null || protoData == null)
    {
      // todo: add a more specific debug log
      Debug.Log("Can't update card game object appearance.");
      return;
    }

    switch(protoData.elementEnum)
    {
      case CardElement.FIRE:
      {
        renderer.material.color = Color.red;
        light.color = Color.red;
        break;
      }
      case CardElement.WIND:
      {
        renderer.material.color = Color.green;
        light.color = Color.green;
        break;
      }
      case CardElement.ICE:
      {
        renderer.material.color = Color.blue;
        light.color = Color.blue;
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
    if(!canPickUp || !InventoryManager.Instance.AutoGiveCard(protoId, count))
    {
      return;
    }

    //gameObject.GetComponent<MeshRenderer

    //if(audioSource)
    //{
    //  audioSource.Play();
    //}

    Destroy(gameObject);
  }
}
