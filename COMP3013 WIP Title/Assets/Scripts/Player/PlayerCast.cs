
using UnityEngine;


[System.Serializable]
public struct ProjectilePair
{
    public CardElement element;
    public GameObject prefab;
}


public class PlayerCast : MonoBehaviour
{

    GameObject player = null;

    [SerializeField] 
    private Transform firingPoint;

    [SerializeField] 
    private float firingSpeed;

    [SerializeField]
    private ProjectilePair[] projectilePrefabs;

    public static PlayerCast Instance;

    private float lastTimeShot = 0;

    void Awake()
    {
        Instance = GetComponent<PlayerCast>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        OnEquipmentInput();
    }

    private void OnEquipmentInput()
    {
        int usedSlot = GetUsedSlotIndex();
        if(usedSlot == -1)
        {
            return;
        }

        CardItem cardItem = EquipmentManager.Instance.GetEquipedCard(usedSlot);
        if(cardItem == null || cardItem.IsType(CardType.PASSIVE))
        {
            return;
        }

        if(--cardItem.currCount <= 0)
        {
            EquipmentManager.Instance.SetCard(usedSlot, null);
        }

        EquipmentManager.Instance.UpdateSlot(usedSlot);
        Shoot(cardItem);


    }

    private int GetUsedSlotIndex()
    {
        // todo: Use input system istead with mapped game actions
        for (int i = 0; i < Constants.MAX_EQUIPMENT_SIZE; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                return i;
            }
        }

        return -1;
    }

    public void Shoot(CardItem cardItem)
    {
        if (lastTimeShot + firingSpeed > Time.time || cardItem == null)
        {
            return;
        }
        else if(cardItem.IsType(CardType.PASSIVE))
        {
            Debug.LogFormat("You can't invoke a passive card.");
            return;
        }

        foreach(ProjectilePair pair in projectilePrefabs)
        {
            if(pair.element != cardItem.element)
            {
                continue;
            }

            SpawnProjectile(pair);
            break;
        }


        lastTimeShot = Time.time;
    }

    private void SpawnProjectile(ProjectilePair projectilePair)
    {
        GameObject projectile = Instantiate(projectilePair.prefab, firingPoint.position, firingPoint.rotation);
        if(projectile == null)
        {
            Debug.LogErrorFormat("Couldn't spawn projectile, instantination failed. Projectile Pair: {0}", projectilePair);
            return;
        }

        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        if(controller == null)
        {
            Debug.LogErrorFormat("Projectile prefab has no ProjectileController script attached, cannot attach attack data! {0}", projectilePair);
            Destroy(projectile);
            return;
        }


        // Maybe add the script to the player game object
        controller.SetAttacker(player);
    }
}
