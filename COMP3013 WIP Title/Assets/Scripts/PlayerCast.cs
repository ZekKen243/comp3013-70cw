using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] 
    private Transform firingPoint;

    [SerializeField] 
    private GameObject projectilePrefab;

    [SerializeField] 
    private float firingSpeed;

    public static PlayerCast Instance;

    private float lastTimeShot = 0;

    void Awake()
    {
        Instance = GetComponent<PlayerCast>();
    }

    // Update is called once per frame
    
    public void Shoot()
    {
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
