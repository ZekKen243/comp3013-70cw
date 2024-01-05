using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ProjectilePair
{
    public CardElement element;
    public GameObject prefab;
}


public class PlayerCast : MonoBehaviour
{

    [SerializeField] 
    private Transform firingPoint;

    [SerializeField] 
    private ProjectilePair[] projectilePrefabs;

    [SerializeField] 
    private float firingSpeed;

    public static PlayerCast Instance;

    private float lastTimeShot = 0;

    void Awake()
    {
        Instance = GetComponent<PlayerCast>();
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

        Shoot(EquipmentManager.Instance.GetEquipedCard(usedSlot));
    }

    private int GetUsedSlotIndex()
    {
        for (int i = 0; i < 4; i++)
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


        foreach(ProjectilePair pair in projectilePrefabs)
        {
            if(pair.element != cardItem.element)
            {
                continue;
            }

            Instantiate(pair.prefab, firingPoint.position, firingPoint.rotation);
        }


        lastTimeShot = Time.time;
    }
}
